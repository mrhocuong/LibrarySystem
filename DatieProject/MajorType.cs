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
    
    public partial class MajorType
    {
        public MajorType()
        {
            this.Accounts = new HashSet<Account>();
            this.Books = new HashSet<Book>();
        }
    
        public int Id { get; set; }
        public string MajorType1 { get; set; }
    
        public virtual ICollection<Account> Accounts { get; set; }
        public virtual ICollection<Book> Books { get; set; }
    }
}
