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
    
    public partial class AlbumType
    {
        public AlbumType()
        {
            this.Albums = new HashSet<Album>();
        }
    
        public int AlbumTypeID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    
        public virtual ICollection<Album> Albums { get; set; }
    }
}