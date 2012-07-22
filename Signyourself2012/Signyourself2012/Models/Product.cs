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
    
    public partial class Product
    {
        public Product()
        {
            this.Purchases = new HashSet<Purchase>();
            this.CampaignComments = new HashSet<CampaignComment>();
            this.CampaignReviews = new HashSet<CampaignReview>();
            this.Credits = new HashSet<Credit>();
            this.Files = new HashSet<File>();
            this.Messages = new HashSet<Message>();
            this.Rewards = new HashSet<Reward>();
            this.UserComments = new HashSet<UserComment>();
        }
    
        public int ProductID { get; set; }
        public System.Guid UserID { get; set; }
        public int ProductTypeID { get; set; }
        public Nullable<int> QTY { get; set; }
        public Nullable<bool> Active { get; set; }
        public Nullable<bool> Approved { get; set; }
        public string ProductName { get; set; }
        public System.DateTime DateAdded { get; set; }
        public string Description { get; set; }
        public string CashPrice { get; set; }
        public Nullable<int> CreditPrice { get; set; }
    
        public virtual ProductType ProductType { get; set; }
        public virtual ICollection<Purchase> Purchases { get; set; }
        public virtual User Seller { get; set; }
        public virtual ICollection<CampaignComment> CampaignComments { get; set; }
        public virtual ICollection<CampaignReview> CampaignReviews { get; set; }
        public virtual ICollection<Credit> Credits { get; set; }
        public virtual ICollection<File> Files { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<Reward> Rewards { get; set; }
        public virtual ICollection<UserComment> UserComments { get; set; }
    }
}
