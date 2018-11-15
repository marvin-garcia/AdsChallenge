using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;

namespace SendClaims
{
    class Program
    {
        public class Claim
        {
            public string text { get; set; }
            public string imageUrl { get; set; }

            public Claim(string text, string imageUrl)
            {
                this.text = text;
                this.imageUrl = imageUrl;
            }
        }

        static void Main(string[] args)
        {
            Claim[] badCLaims = new Claim[]
            {
                new Claim(
                    "The guy was all over the road and I had to swerve a number of times before I hit him.",
                    "https://dss.fosterwebmarketing.com/upload/hsinjurylaw.com/pedestrian-accident.jpg"),
                new Claim(
                    "The rain came so hard that our gutters were clogged and water began to collect on second story patio.It collapsed with the weight.",
                    "http://www.swc.nd.gov/info_edu/galleries/Rice%20Lake%20Flooding/photo%202.jpg"),
            };

            Claim[] claims = new Claim[]
            {
                new Claim(
                    "Coming home, I drove into the wrong house and collided with a tree I don't have.",
                    "https://mediaassets.news5cleveland.com/photo/2017/09/07/Broadway%20crash_1504787624689_65836660_ver1.0_640_480.jpg"),
                new Claim(
                    "The other car collided with mine without givng warning of its intentions.",
                    "http://nnimgt-a.akamaihd.net/transform/v1/crop/frm/xZTdZ6tEPcDnFXvZB8aTeZ/c9594070-c8d9-40f9-9b79-81d0c3e1a9f7.jpg/r0_376_4032_2643_w1200_h678_fmax.jpg"),
                new Claim(
                    "I thought my window was down, but I found out it was up when I put my head through it.",
                    "http://www.whitehouse51.com/pic/proevalue.com/wp-content/uploads/2012/02/broken-window-2.jpg"),
                new Claim(
                    "I collided with a stationary truck coming the other way.",
                    "https://i.ytimg.com/vi/DVNJF4v9Ow8/maxresdefault.jpg"),
                new Claim(
                    "A truck backed through my windshield into my wife's face.",
                    "http://www.samacharnama.com/wp-content/uploads/2018/02/fatal_car_truck.jpg"),
                new Claim(
                    "A pedestrian hit me and went under my car.",
                    "https://www.phxinjurylaw.com/wp-content/uploads/2010/06/blog-crash.jpg"),
                new Claim(
                    "I pulled away from the side of the road, glanced at my mother-in-law and headed over the embankment.",
                    "http://pulsoslp.com.mx/wp-content/uploads/2015/06/carro-acaba-en-cu-680x330.jpg"),
                new Claim(
                    "In an attempt to kill a fly I drove into a telephone pole.",
                    "http://media.morristechnology.com/webmedia/upload/santa_clarita/article/2014/03/12/0313_car_vs_pole.jpg"),
                new Claim(
                    "I had been driving for forty years when I fell asleep at the wheel and had an accident.",
                    "https://we-ha.com/wp-content/uploads/2016/08/car-hits-telephone-pole.jpg"),
                new Claim(
                    "As I approached the intersection a sign suddenly appeared where no STOP sign had ever appeared before.",
                    "https://bklyner.com/wp-content/uploads/2013/06/IMG_2176.jpg"),
                new Claim(
                    "My car was legally parked as it backed into the other car.",
                    "https://bklyner.com/wp-content/uploads/2013/06/IMG_2176.jpg"),
                new Claim(
                    "An invisible car came out of nowhere, struck my car and vanished.",
                    "https://1bz7y91uciwq1a1xek1b3w25-wpengine.netdna-ssl.com/wp-content/uploads/2017/03/iStock-540122448-1024x683.jpg"),
                new Claim(
                    "I told the police that I was not injured but on removing my hat, found that I had fractured my skull.",
                    "https://i1.wp.com/www.mrgoglass.com/wp-content/uploads/2017/12/my-windshield-cracked-for-now-reason.jpg?fit=1020%2C680&ssl=1"),
                new Claim(
                    "The pedestrian had no idea which direction to run, so I ran over him.",
                    "https://nnimg-a.akamaihd.net/silverstone-feed-data/a514207d-dd85-4054-995e-4844b8507a76.jpg"),
                new Claim(
                    "I saw a slow moving, sad old faced gentleman as he bounced off the roof of my car.",
                    "https://i1.wp.com/www.mrgoglass.com/wp-content/uploads/2017/12/my-windshield-cracked-for-now-reason.jpg?fit=1020%2C680&ssl=1"),
                new Claim(
                    "I was thrown from my car as it left the road.I was later found in a ditch by some stray cows.",
                    "https://i1.wp.com/www.mrgoglass.com/wp-content/uploads/2017/12/my-windshield-cracked-for-now-reason.jpg?fit=1020%2C680&ssl=1"),
                new Claim(
                    "I was driving down El Camino and stopped at a red light.It was about 3pm in the afternoon.The sun was bright and shining just behind the stoplight.This made it hard to see the lights.There was a car on my left in the left turn lane.A few moments later another car, a black sedan pulled up behind me.When the left turn light changed green, the black sedan hit me thinking that the light had changed for us, but I had not moved because the light was still red.After hitting my car, the black sedan backed up and then sped past me.I did manage to catch its license plate.The license plate of the black sedan was ABC123.",
                    "https://i1.wp.com/www.mrgoglass.com/wp-content/uploads/2017/12/my-windshield-cracked-for-now-reason.jpg?fit=1020%2C680&ssl=1"),
                new Claim(
                    "I caught the end of the yellow light and the other car moved into the intersection before the light had turned green. I clipped its fender.",
                    "http://www.samacharnama.com/wp-content/uploads/2018/02/fatal_car_truck.jpg"),
                new Claim(
                    "It was dark and I did not see the stop sign.I ran straight into the car crossing the intersection in front of me.",
                    "http://www.samacharnama.com/wp-content/uploads/2018/02/fatal_car_truck.jpg"),
                new Claim(
                    "The tornado was devastating, it tore the roof off of our house and the wind took with it all of hour precious paintings.",
                    "https://www.spc.noaa.gov/faq/tornado/f1.jpg"),
                new Claim(
                    "The house was under five feet of water, and the first floor was completely flooded.",
                    "https://www.advancedenergy.org/wp-content/uploads/2018/09/FEMA_-_31869_-_Flooded_neighborhood_crooked_mail_box_in_Oklahoma.jpg"),
                new Claim(
                    "In the torrential rains, the hillside behind our home turned into a mudslide. It demolished one wall.",
                    "https://i.stack.imgur.com/6NGyA.jpg"),
                new Claim(
                    "The snow began to pile up so high that the tree in our front yard fell over on to our roof.",
                    "http://www.homesteadtreeservice.com/images/tree%20felled%20on%20house.gif"),
                new Claim(
                    "Our home's roof could not withstand the weight of all the snow that had piled on top of it.",
                    "http://www.chicagoflatroofcompany.com/wp-content/uploads/2016/09/Snow-collapse-1024x484.png"),
                new Claim(
                    "The strong winds ripped open our front door and sent everything inside the house flying.All of our fragile antiques were shattered.",
                    "http://www.realestatejunkie.com/blog/wp-content/uploads/2016/10/hurricanedamage.jpeg"),
                new Claim(
                    "There was strong rumble from the earthquake and then we heard a crack as the patio collapsed into the ocean.",
                    "https://disastersafety.org/wp-content/uploads/FEMA-earthquake3-e1432757322417.jpg"),
                new Claim(
                    "The earthquake create a large crack in our cieling.",
                    "https://disastersafety.org/wp-content/uploads/FEMA-earthquake3-e1432757322417.jpg"),
                new Claim(
                    "After the quake, our foundation of has a crack in it that runs the length of the house.",
                    "https://disastersafety.org/wp-content/uploads/FEMA-earthquake3-e1432757322417.jpg"),
                new Claim(
                    "The burglars broke in to the house through our living room window.",
                    "https://disastersafety.org/wp-content/uploads/FEMA-earthquake3-e1432757322417.jpg"),
                new Claim(
                    "The thieves came in via the sliding glass door in the backyard which was unlocked.",
                    "https://disastersafety.org/wp-content/uploads/FEMA-earthquake3-e1432757322417.jpg"),
                new Claim(
                    "We came home to find all of our computers missing. The thieves entered through the french doors.",
                    "https://zoomingjapan.com/photos/tohoku/miyagi/ishinomaki/IMGP2112.jpg"),
                new Claim(
                    "We left a candle on in the bathroom. A towel hanging nearby caught fire and then burned the whole bathroom.",
                    "https://image.shutterstock.com/image-photo/burned-house-inside-hallway-home-260nw-559304629.jpg"),
                new Claim(
                    "The oven was left on as everyone forgot to turn it off.The turkey caught fire and the intense heat melted the stovetop.",
                    "https://image.shutterstock.com/image-photo/burned-house-inside-hallway-home-260nw-559304629.jpg"),
                new Claim(
                    "The christmas tree lights short circuited and the christmas tree caught fire.",
                    "https://image.shutterstock.com/image-photo/burned-house-inside-hallway-home-260nw-559304629.jpg"),
                new Claim(
                    "There was a gas leak in the house when nobody was home. A spark must have triggered the explosion.",
                    "https://image.shutterstock.com/image-photo/burned-house-inside-hallway-home-260nw-559304629.jpg"),
                new Claim(
                    "The termite damage caused the wood supporting the awning to weaken.In the strong rains, it collapsed.",
                    "https://www.thebugmaster.com/wp-content/uploads/2015/12/termite-damage.jpg"),
                new Claim(
                    "The skylight had a leak and water dripped into the house destroying our persian carpet.",
                    "http://www.carpetcleaningport.com/wp-content/uploads/2014/04/Water-damage-3.jpg"),
                new Claim(
                    "The offshore wind was so strong it ripped the awning off of the house.",
                    "http://www.maxwallpro.com/wp-content/uploads/dscf0499-1-862x647.jpg"),
            };

            using (var client = new HttpClient())
            {
                string endpoint = "http://localhost:7071/api/Function1";

                for (int i = 0; i < claims.Length; i++)
                {
                    var content = new StringContent(
                        JsonConvert.SerializeObject(claims[i]),
                        Encoding.UTF8,
                        "application/json");

                    Console.WriteLine($"Claim {i + 1}/{claims.Length}. Sending...");

                    var response = client.PostAsync(endpoint, content).ContinueWith((r) =>
                    {
                        return r.Result;
                    }).Result;

                    Console.WriteLine($"Claim {i + 1}/{claims.Length}. Text: {claims[i].text.Substring(10)}. Response: {response.StatusCode}");

                    if (!response.IsSuccessStatusCode)
                    {
                        bool failed = true;
                    }
                }
            }
        }
    }
}
