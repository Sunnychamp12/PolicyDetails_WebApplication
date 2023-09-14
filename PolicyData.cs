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
    
    public partial class PolicyData
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PolicyData()
        {
            this.PolicyTransactions = new HashSet<PolicyTransaction>();
        }
    
        public int PolicyKey { get; set; }
        public int ContractNumber { get; set; }
        public string CustomerCode { get; set; }
        public System.DateTime RiskCommencementDate { get; set; }
        public string ProductName { get; set; }
        public System.DateTime MaturityDate { get; set; }
        public System.DateTime NextRenewalDue { get; set; }
        public decimal SumAssuredAmount { get; set; }
        public decimal? PremiumAmount { get; set; }
        public decimal ContractStatusCode { get; set; }
        public string PolicyStatus { get; set; }
        public System.DateTime ETLDate { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PolicyTransaction> PolicyTransactions { get; set; }
    }
}
