using MediaBrowser.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PolicyDetails_WebApplication.Models
{
    public class AccountTransactionRequest
    {
        [Required]
        public int AccountNo { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }
    }
}