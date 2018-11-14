using System;
using System.Collections.Generic;
using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;

namespace ContosoFunction.Models
{
    public class ClaimDocument
    {
        //[System.ComponentModel.DataAnnotations.Key]
        //[IsFilterable]
        public string Guid { get; set; }

        public string ImageUrl { get; set; }

        //[IsSearchable, Analyzer("en.microsoft")]
        public string Claim { get; set; }

        //[IsSearchable, Analyzer("en.microsoft")]
        public string Summary { get; set; }

        //[IsFilterable, IsSearchable, IsFacetable]
        public double Sentiment { get; set; }

        public string Category { get; set; }

        //[IsSearchable]
        public string[] Tags { get; set; }
    }
}
