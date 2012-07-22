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
    
    public partial class UserComment
    {
        public int UserCommentID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public System.DateTime DateCreated { get; set; }
        public bool Approved { get; set; }
        public Nullable<int> InReplyTo { get; set; }
        public bool isSpam { get; set; }
        public Nullable<bool> Spaminess { get; set; }
        public bool ReplyEnabled { get; set; }
        public System.Guid AuthorID { get; set; }
        public System.Guid ReceiverID { get; set; }
        public Nullable<bool> IsDeactivated { get; set; }
        public Nullable<int> ProductID { get; set; }
    
        public virtual User Author { get; set; }
        public virtual User User { get; set; }
        public virtual Product Product { get; set; }
    }
}
