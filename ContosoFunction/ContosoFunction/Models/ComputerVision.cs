using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContosoFunction.Models
{
    public class ComputerVision
    {
        public class Request
        {
            public string url { get; set; }

            public Request(string url)
            {
                this.url = url;
            }
        }

        public class Response
        {
            public string requestId { get; set; }
            public Metadata metadata { get; set; }
            public Tag[] tags { get; set; }

            public class Metadata
            {
                public double width { get; set; }
                public double height { get; set; }
                public string format { get; set; }
            }

            public class Tag
            {
                public string name { get; set; }
                public double confidence { get; set; }
            }
        }
    }
}
