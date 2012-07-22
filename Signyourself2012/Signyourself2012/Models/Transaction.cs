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
    
    public partial class Transaction
    {
        public int PurchaseID { get; set; }
        public int TransactionTypeID { get; set; }
        public string Amount { get; set; }
        public System.DateTime Date { get; set; }
        public int TransactionID { get; set; }
        public Nullable<int> SendingAccountID { get; set; }
        public Nullable<int> ReceivingAcountID { get; set; }
    
        public virtual Account SendingAccount { get; set; }
        public virtual Purchase Purchase { get; set; }
        public virtual TransactionType TransactionType { get; set; }
        public virtual Account ReceivingAcount { get; set; }
    }
}
