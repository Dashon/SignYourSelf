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
    
    public partial class PrivacyLevel
    {
        public PrivacyLevel()
        {
            this.CampaignReviews = new HashSet<CampaignReview>();
            this.CampaignUpdates = new HashSet<CampaignUpdate>();
            this.Files = new HashSet<File>();
            this.FileDownloadRules = new HashSet<File>();
            this.Profiles = new HashSet<Profile>();
            this.UserMesages = new HashSet<Profile>();
            this.UserCalls = new HashSet<Profile>();
            this.CampaignEmails = new HashSet<Campaign>();
            this.Campaigns = new HashSet<Campaign>();
            this.Albums = new HashSet<Album>();
        }
    
        public int PrivacyLevelID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    
        public virtual ICollection<CampaignReview> CampaignReviews { get; set; }
        public virtual ICollection<CampaignUpdate> CampaignUpdates { get; set; }
        public virtual ICollection<File> Files { get; set; }
        public virtual ICollection<File> FileDownloadRules { get; set; }
        public virtual ICollection<Profile> Profiles { get; set; }
        public virtual ICollection<Profile> UserMesages { get; set; }
        public virtual ICollection<Profile> UserCalls { get; set; }
        public virtual ICollection<Campaign> CampaignEmails { get; set; }
        public virtual ICollection<Campaign> Campaigns { get; set; }
        public virtual ICollection<Album> Albums { get; set; }
    }
}
