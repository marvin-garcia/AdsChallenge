using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContosoFunction.Models
{
    public class CustomVision
    {
        public class Request
        {
            public string Url { get; set; }

            public Request(string url)
            {
                this.Url = url;
            }
        }

        public class Response
        {
            public string id { get; set; }
            public string project { get; set; }
            public string iteration { get; set; }
            public DateTime created { get; set; }
            public Prediction[] predictions { get; set; }

            public class Prediction
            {
                public string tagId { get; set; }
                public string tagName { get; set; }
                public double probability { get; set; }
            }
        }
    }
}
