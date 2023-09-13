using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PolicyDetails_WebApplication.Models
{
    public class SuccessResponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public List<PolicyData_1> PolicyDetails { get; set; }
    }
}