using System;
using System.Net;
using System.Text;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using ContosoFunction.Models;
using System.Text.RegularExpressions;

namespace ContosoFunction
{
    public static class Function1
    {
        #region private variables
        private static readonly string TextSummaryEndpoint = "http://nltk.azurewebsites.net/";
        private static readonly string ComputerVisionEndpoint = "https://eastus.api.cognitive.microsoft.com/vision/v2.0/tag?language=en";
        private static readonly string ComputerVisionApiKey = "c0d79a086d184cfda8c79f4e4d9d284b";
        private static readonly double ComputerVisionConfidence = 0.6;
        private static readonly string CustomVisionEndpoint = "https://southcentralus.api.cognitive.microsoft.com/customvision/v2.0/Prediction/41cbac4e-8634-4a7e-8926-e0022e668362/url?iterationId=fe0110dd-dd92-4a40-a723-bf187f3278fa";
        private static readonly string CustomVisionApiKey = "7a966ec07feb4be785b1944b2b2cd5c4";
        private static readonly string TextAnalyticsEndpoint = "https://eastus.api.cognitive.microsoft.com/text/analytics/v2.0/sentiment";
        private static readonly string TextAnalyticsApiKey = "56382ddc1f8842d7a5a897e9eb937638";
        private static readonly string RunIndexerEndpoint = "https://adschallengepoc.search.windows.net/indexers/claimsindexer/run?api-version=2017-11-11";
        private static readonly string SearchApiKey = "A46B38C64CD5E842A0727B126193E2C3";
        #endregion

        [FunctionName("Function1")]
        public static async Task<ClaimDocument> Run(
            [HttpTrigger(
                WebHookType = "genericJson")] HttpRequestMessage req,
            [DocumentDB(
                databaseName: "ContosoInc",
                collectionName: "Claims",
                ConnectionStringSetting = "CosmosDBConnection")] IAsyncCollector<ClaimDocument> document,
            TraceWriter log)
        {
            try
            {
                log.Info($"Webhook was triggered!");

                string jsonContent = await req.Content.ReadAsStringAsync();
                dynamic data = JsonConvert.DeserializeObject(jsonContent);

                string imageUrl = data.imageUrl;
                string text = data.text;

                #region normalize claim
                string normalizedText = Regex.Replace(text, @"\. *([a-z0-9])", ". $1", RegexOptions.IgnoreCase);
                #endregion

                #region Get claim summary
                string summary;

                if (normalizedText.Split('.').Count() > 3)
                {
                    using (var client = new HttpClient())
                    {
                        var body = new
                        {
                            txt = normalizedText,
                        };
                        var content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");
                        var response = await client.PostAsync(TextSummaryEndpoint, content);

                        if (!response.IsSuccessStatusCode)
                            throw new Exception($"Failed to get claim summary. Status code: {response.StatusCode}. Reason phrase: {response.ReasonPhrase}");

                        var responseString = await response.Content.ReadAsStringAsync();
                        string[] summaries = JsonConvert.DeserializeObject<string[]>(responseString);
                        summary = summaries[0];
                    }
                }
                else
                    summary = normalizedText;
                #endregion

                #region Get claim sentiment
                double sentiment;

                using (var client = new HttpClient())
                {
                    var body = new TextAnalytics.Request(1, normalizedText);
                    var content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");
                    client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", TextAnalyticsApiKey);
                    var response = await client.PostAsync(TextAnalyticsEndpoint, content);

                    if (!response.IsSuccessStatusCode)
                        throw new Exception($"Failed to get claim sentiment. Status code: {response.StatusCode}. Reason phrase: {response.ReasonPhrase}");

                    string responseString = await response.Content.ReadAsStringAsync();
                    var textAnalyticsResponse = JsonConvert.DeserializeObject<TextAnalytics.Response>(responseString);

                    sentiment = textAnalyticsResponse.documents.Where(x => x.id == 1).FirstOrDefault().score;
                }
                #endregion

                #region Get image tags
                string[] tags;

                using (var client = new HttpClient())
                {
                    var body = new ComputerVision.Request(imageUrl);
                    var content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");
                    client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", ComputerVisionApiKey);
                    var response = await client.PostAsync(ComputerVisionEndpoint, content);

                    if (!response.IsSuccessStatusCode)
                        throw new Exception($"Failed to get image tags. Status code: {response.StatusCode}. Reason phrase: {response.ReasonPhrase}");

                    string responseString = await response.Content.ReadAsStringAsync();
                    var computerVisionResponse = JsonConvert.DeserializeObject<ComputerVision.Response>(responseString);

                    tags = computerVisionResponse.tags.Where(x => x.confidence > ComputerVisionConfidence).Select(x => x.name).ToArray();
                }
                #endregion

                #region Get image category
                double autoProbability;
                double homeProbability;

                using (var client = new HttpClient())
                {
                    var body = new ComputerVision.Request(imageUrl);
                    var content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");
                    client.DefaultRequestHeaders.Add("Prediction-Key", CustomVisionApiKey);
                    var response = await client.PostAsync(CustomVisionEndpoint, content);

                    if (!response.IsSuccessStatusCode)
                        throw new Exception($"Failed to classify image. Status code: {response.StatusCode}. Reason phrase: {response.ReasonPhrase}");

                    string responseString = await response.Content.ReadAsStringAsync();
                    var customVisionResponse = JsonConvert.DeserializeObject<CustomVision.Response>(responseString);

                    autoProbability = customVisionResponse.predictions.Where(x => string.Equals(x.tagName, "auto", StringComparison.OrdinalIgnoreCase)).First().probability;
                    homeProbability = customVisionResponse.predictions.Where(x => string.Equals(x.tagName, "home", StringComparison.OrdinalIgnoreCase)).First().probability;
                }
                #endregion

                #region Store in DB
                ClaimDocument claimDocument = new ClaimDocument()
                {
                    Guid = Guid.NewGuid().ToString(),
                    ImageUrl = imageUrl,
                    Claim = normalizedText,
                    Summary = summary,
                    Sentiment = sentiment,
                    AutoProbability = autoProbability,
                    HomeProbability = homeProbability,
                    Tags = tags,
                };

                await document.AddAsync(claimDocument);
                #endregion

                #region Update indexer
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("api-key", SearchApiKey);
                    var response = await client.PostAsync(RunIndexerEndpoint, null);

                    if (response.StatusCode != HttpStatusCode.Accepted)
                        throw new Exception($"Failed to run indexer. Status code: {response.StatusCode}. Reason phrase: {response.ReasonPhrase}");
                }
                #endregion

                return claimDocument;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
