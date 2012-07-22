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
    
    public partial class CampaignReview
    {
        public int CampaignReviewID { get; set; }
        public int CampaignID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public System.DateTime DateCreated { get; set; }
        public bool Approved { get; set; }
        public Nullable<int> InReplyTo { get; set; }
        public bool PayLocked { get; set; }
        public Nullable<double> Rating { get; set; }
        public bool ReplyEnabled { get; set; }
        public System.Guid UserID { get; set; }
        public Nullable<bool> LikeLocked { get; set; }
        public int PrivacyLevelID { get; set; }
        public Nullable<bool> IsDeactivated { get; set; }
        public Nullable<int> ProductID { get; set; }
    
        public virtual Campaign Campaign { get; set; }
        public virtual User User { get; set; }
        public virtual PrivacyLevel PrivacyLevel { get; set; }
        public virtual Product Product { get; set; }
    }
}