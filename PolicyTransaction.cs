//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PolicyDetails_WebApplication
{
    using System;
    using System.Collections.Generic;
    
    public partial class PolicyTransaction
    {
        public int PolicyNo { get; set; }
        public int CustomerId { get; set; }
        public Nullable<decimal> PremiumAmount { get; set; }
        public System.DateTime PremiumDate { get; set; }
        public string PolicyStatus { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> EditDate { get; set; }
    
        public virtual PolicyData PolicyData { get; set; }
    }
}