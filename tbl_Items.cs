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
    
    public partial class tbl_Items
    {
        public int ItemId { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public string ItemName { get; set; }
        public Nullable<decimal> Rate { get; set; }
    
        public virtual tbl_Category tbl_Category { get; set; }
    }
}