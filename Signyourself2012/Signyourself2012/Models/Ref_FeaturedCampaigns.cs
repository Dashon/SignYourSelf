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
    
    public partial class Ref_FeaturedCampaigns
    {
        public int FeaturedCampaignID { get; set; }
        public int CampaignID { get; set; }
    
        public virtual Campaign Campaign { get; set; }
    }
}
