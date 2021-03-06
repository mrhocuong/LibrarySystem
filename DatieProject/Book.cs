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
    
    public partial class Book
    {
        public Book()
        {
            this.BookDetails = new HashSet<BookDetail>();
            this.TagDetails = new HashSet<TagDetail>();
        }
    
        public string ISBN { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public Nullable<int> Major { get; set; }
        public Nullable<int> TypeBook { get; set; }
        public Nullable<int> AvailableInVault { get; set; }
        public Nullable<int> Amount { get; set; }
        public Nullable<bool> IsDelete { get; set; }
    
        public virtual MajorType MajorType { get; set; }
        public virtual TypeBook TypeBook1 { get; set; }
        public virtual ICollection<BookDetail> BookDetails { get; set; }
        public virtual ICollection<TagDetail> TagDetails { get; set; }
    }
}
