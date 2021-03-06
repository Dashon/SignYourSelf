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
    
    public partial class Campaign
    {
        public Campaign()
        {
            this.CampaignComments = new HashSet<CampaignComment>();
            this.CampaignReviews = new HashSet<CampaignReview>();
            this.CampaignUpdates = new HashSet<CampaignUpdate>();
            this.FeaturedCampaigns = new HashSet<Ref_FeaturedCampaigns>();
            this.Tags = new HashSet<Tag>();
            this.Goals = new HashSet<Goal>();
            this.Albums = new HashSet<Album>();
            this.CampaignUsers = new HashSet<Ref_CampaignUsers>();
        }
    
        public int CampaignID { get; set; }
        public string Name { get; set; }
        public int CampaignTypeId { get; set; }
        public string Description { get; set; }
        public System.DateTime DateCreated { get; set; }
        public System.Guid UserId { get; set; }
        public bool Active { get; set; }
        public bool Appoved { get; set; }
        public string Email { get; set; }
        public string Location { get; set; }
        public System.DateTime EndDate { get; set; }
        public Nullable<int> AccountId { get; set; }
        public int GenreID { get; set; }
        public string Intro { get; set; }
        public string Avatar { get; set; }
        public Nullable<int> EmailPrivacyLevelID { get; set; }
        public Nullable<int> CampaignPrivacyID { get; set; }
        public Nullable<bool> IsDeactivated { get; set; }
    
        public virtual Account Account { get; set; }
        public virtual ICollection<CampaignComment> CampaignComments { get; set; }
        public virtual ICollection<CampaignReview> CampaignReviews { get; set; }
        public virtual CampaignType CampaignType { get; set; }
        public virtual Genre Genre { get; set; }
        public virtual ICollection<CampaignUpdate> CampaignUpdates { get; set; }
        public virtual ICollection<Ref_FeaturedCampaigns> FeaturedCampaigns { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
        public virtual User Creator { get; set; }
        public virtual ICollection<Goal> Goals { get; set; }
        public virtual PrivacyLevel EmailPrivacyLevel { get; set; }
        public virtual PrivacyLevel CampaignPrivacyLevel { get; set; }
        public virtual ICollection<Album> Albums { get; set; }
        public virtual ICollection<Ref_CampaignUsers> CampaignUsers { get; set; }
    }
}
