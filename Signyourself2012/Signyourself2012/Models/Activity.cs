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
    
    public partial class Activity
    {
        public int ActivityID { get; set; }
        public int ActivityTypeID { get; set; }
        public string Comment { get; set; }
        public System.Guid UserID { get; set; }
        public int itemID { get; set; }
        public System.DateTime DateTime { get; set; }
    
        public virtual ActivityType ActivityType { get; set; }
        public virtual User User { get; set; }
    }
}