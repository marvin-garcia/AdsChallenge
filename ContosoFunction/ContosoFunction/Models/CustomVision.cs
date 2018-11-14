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
            public string url { get; set; }

            public Request(string url)
            {
                this.url = url;
            }
        }

        public class Response
        {

        }
    }
}
