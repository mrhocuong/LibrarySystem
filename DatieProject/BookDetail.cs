//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DatieProject
{
    using System;
    using System.Collections.Generic;
    
    public partial class BookDetail
    {
        public BookDetail()
        {
            this.OrderDetails = new HashSet<OrderDetail>();
        }
    
        public string Id { get; set; }
        public string ISBN { get; set; }
        public string Edition { get; set; }
        public Nullable<bool> IsBorrowed { get; set; }
        public Nullable<bool> IsDelete { get; set; }
    
        public virtual Book Book { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}