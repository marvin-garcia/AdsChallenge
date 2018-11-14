using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContosoFunction.Models
{
    public class TextAnalytics
    {
        public class Request
        {
            public Document[] documents { get; set; }

            public class Document
            {
                public int id { get; set; }
                public string language { get; set; }
                public string text { get; set; }

                public Document(int id, string text, string language = "en")
                {
                    this.id = id;
                    this.language = language;
                    this.text = text;
                }
            }

            public Request(int id, string text, string language = "en")
            {
                this.documents = new Document[]
                {
                    new Document(id, text, language)
                };
            }
        }

        public class Response
        {
            public Document[] documents { get; set; }
            public string[] errors { get; set; }
            public double Where { get; internal set; }

            public class Document
            {
                public int id { get; set; }
                public double score { get; set; }
            }
        }
    }
}
