using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PolicyDetails_WebApplication.Models
{
    public class GetSuccessResponse
    {
        public int Status { get; set; }
        public List<PolicyData> PolicyDetails { get; set; }
    }
    public class SuccessResponse
    {
        public int Status { get; set; }
        public string Message { get; set; }
    }
    public class GetPolicyTransactionResponse
    {
        public int Status { get; set; }
        public List<CustomerDetails> customerDetails { get; set; }
    }
}