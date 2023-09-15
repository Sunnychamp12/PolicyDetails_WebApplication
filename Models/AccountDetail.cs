using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PolicyDetails_WebApplication.Models
{
    public class AccountDetail
    {
        public int AccountNo { get; set; }
        public string AccountName { get; set; }
        public string Address { get; set; }
        public string AccountDescription { get; set; }
        public decimal? OpeningBalance { get; set; }
        public decimal? ClosingBalance { get; set; }
        public string Branch { get; set; }
        public string IFSCode { get; set; }
        public List<Transaction> Transactions { get; set; }
    }
}