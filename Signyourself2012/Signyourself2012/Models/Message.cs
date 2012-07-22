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
    
    public partial class Message
    {
        public int MessageID { get; set; }
        public System.DateTime DateCreated { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public Nullable<int> InReplyToMesssageId { get; set; }
        public Nullable<bool> Read { get; set; }
        public Nullable<System.DateTime> DateRead { get; set; }
        public System.Guid UserID { get; set; }
        public bool PayLocked { get; set; }
        public bool LikeLocked { get; set; }
        public System.Guid RecipientID { get; set; }
        public Nullable<int> ProductID { get; set; }
    
        public virtual User Reciepient { get; set; }
        public virtual User Sender { get; set; }
        public virtual Product Product { get; set; }
    }
}
