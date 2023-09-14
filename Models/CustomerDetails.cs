using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PolicyDetails_WebApplication.Models
{
    public class CustomerDetails
    {
        public int CustomerId { get; set; }
        public int PolicyNo { get; set; }
        public decimal? PremiumAmount { get; set; }
        public DateTime PremiumDate { get; set; }
        public string PolicyStatus { get; set; }
        public List<PolicyData> policyDetails { get; set; }
    }
}