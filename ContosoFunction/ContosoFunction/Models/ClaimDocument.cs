using System;
using System.Collections.Generic;
using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;

namespace ContosoFunction.Models
{
    public class ClaimDocument
    {
        public string Guid { get; set; }
        public string ImageUrl { get; set; }
        public string Claim { get; set; }
        public string Summary { get; set; }
        public double Sentiment { get; set; }
        public double AutoProbability { get; set; }
        public double HomeProbability { get; set; }
        public string[] Tags { get; set; }
    }
}
