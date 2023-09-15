using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PolicyDetails_WebApplication.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public Nullable<int> AccountNo { get; set; }
        public Nullable<System.DateTime> TransDate { get; set; }
        public string Details { get; set; }
        public Nullable<decimal> Debit { get; set; }
        public Nullable<decimal> Credit { get; set; }
        public Nullable<decimal> Balance { get; set; }
    }
}