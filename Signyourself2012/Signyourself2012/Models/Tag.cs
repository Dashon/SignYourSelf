//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Signyourself2012.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Tag
    {
        public Tag()
        {
            this.Campaigns = new HashSet<Campaign>();
            this.Files = new HashSet<File>();
            this.Albums = new HashSet<Album>();
        }
    
        public int TagID { get; set; }
        public string Name { get; set; }
    
        public virtual ICollection<Campaign> Campaigns { get; set; }
        public virtual ICollection<File> Files { get; set; }
        public virtual ICollection<Album> Albums { get; set; }
    }
}
