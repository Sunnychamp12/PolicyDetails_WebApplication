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
    
    public partial class tbl_AccountDetails
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_AccountDetails()
        {
            this.tbl_Transaction = new HashSet<tbl_Transaction>();
        }
    
        public int AccountNo { get; set; }
        public string AccountName { get; set; }
        public string Address { get; set; }
        public string AccountDescription { get; set; }
        public string Branch { get; set; }
        public string IFSCode { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<System.DateTime> EditDate { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Transaction> tbl_Transaction { get; set; }
    }
}