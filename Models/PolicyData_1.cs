using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PolicyDetails_WebApplication.Models
{
    public class PolicyData_1
    {
        [Key]
        public int PolicyKey { get; set; }
        public int ContractNumber { get; set; }
        public string CustomerCode { get; set; }
        public DateTime RiskCommencementDate { get; set; }
        public string ProductName { get; set; }
        public DateTime MaturityDate { get; set; }
        public DateTime NextRenewalDue { get; set; }
        public decimal SumAssuredAmount { get; set; }
        public decimal PremiumAmount { get; set; }
        public decimal ContractStatusCode { get; set; }
        public string PolicyStatus { get; set; }
        public DateTime ETLDate { get; set; }
    }
}