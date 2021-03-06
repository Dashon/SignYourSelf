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
    
    public partial class Profile
    {
        public System.Guid UserId { get; set; }
        public System.DateTime LastUpdatedDate { get; set; }
        public string Blurb { get; set; }
        public string Title { get; set; }
        public string Skills { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Middle { get; set; }
        public Nullable<System.DateTime> DOB { get; set; }
        public string Avatar { get; set; }
        public string Location { get; set; }
        public Nullable<int> PrivacyLevelID { get; set; }
        public Nullable<int> MessagePrivacyLevelID { get; set; }
        public Nullable<int> CallPrivacyLevelID { get; set; }
        public Nullable<bool> IsDeactivated { get; set; }
        public Nullable<int> EmailPrivacyLevelID { get; set; }
    
        public virtual User User { get; set; }
        public virtual PrivacyLevel PrivacyLevel { get; set; }
        public virtual PrivacyLevel MessagePrivacyLevel { get; set; }
        public virtual PrivacyLevel CallPrivacyLevel { get; set; }
        public virtual PrivacyLevel PrivacyLevel3 { get; set; }
    }
}
