using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace Signyourself2012.Models
{
    [MetadataType(typeof(RewardMetaData))]
    public partial class Reward
    { }

    [MetadataType(typeof(UserCommentMetaData))]
    public partial class UserComment
    { }

    [MetadataType(typeof(ProfileMetaData))]
    public partial class Profile
    { }

    [MetadataType(typeof(ProductMetaData))]
    public partial class Product
    { }

    [MetadataType(typeof(MessageMetaData))]
    public partial class Message
    { }

    [MetadataType(typeof(GoalMetaData))]
    public partial class Goal
    { }

    [MetadataType(typeof(FileMetaData))]
    public partial class File
    { }

    [MetadataType(typeof(CampaignReviewMetaData))]
    public partial class CampaignReview
    { }

    [MetadataType(typeof(CampaignCommentMetaData))]
    public partial class CampaignComment
    { }

    [MetadataType(typeof(CampaignMetaData))]
    public partial class Campaign
    { }

    [MetadataType(typeof(AlbumMetaData))]
    public partial class Album
    { }

    [MetadataType(typeof(AddressMetaData))]
    public partial class Address
    { }


    public class RewardMetaData
    {

        [MaxLength(140, ErrorMessage = "Must Be Less Than 140 Charcters")]
        [MinLength(20, ErrorMessage = "Please Enter An Better Details")]
        [Required(ErrorMessage = "Please Enter An Intro Statment")]
        public string Description { get; set; }

        [RegularExpression(MyVars.CurrencyRegx, ErrorMessage = "Must be greater than 0 ")]
        public string Minimum { get; set; }

        [Required(ErrorMessage = "Please Enter A Delivery Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public System.DateTime DeliveryDate { get; set; }

        [RegularExpression("([0-9]+)", ErrorMessage = "Must be greater than 0 (0=Unlimited)")]
        [HtmlProperties(Min = 0)]
        public int MaxAvailable { get; set; }

    }
    public class UserCommentMetaData
    {
        public string Title { get; set; }

        [Required(ErrorMessage = "Please Enter A Comment")]
        public string Content { get; set; }
    }

    public class ProfileMetaData
    {
        [MaxLength(140, ErrorMessage = "Must Be Less Than 140 Charcters")]
        [MinLength(20, ErrorMessage = "Please Enter More About You")]
        public string Blurb { get; set; }
        [MaxLength(50, ErrorMessage = "Must Be Less Than 50 Charcters")]
        public string Title { get; set; }

        public string Skills { get; set; }
        [MaxLength(20, ErrorMessage = "Must Be Less Than 20 Charcters")]
        public string FirstName { get; set; }
        [MaxLength(20, ErrorMessage = "Must Be Less Than 20 Charcters")]
        public string LastName { get; set; }
        [MaxLength(20, ErrorMessage = "Must Be Less Than 20 Charcters")]
        public string Middle { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public Nullable<System.DateTime> DOB { get; set; }

        [DataType(DataType.ImageUrl)]
        public string Avatar { get; set; }

        [MaxLength(40, ErrorMessage = "Must Be Less Than 40 Charcters")]
        public string Location { get; set; }
    }

    public class ProductMetaData
    {
        [RegularExpression(MyVars.CurrencyRegx, ErrorMessage = "Must be greater than 0 ")]
        public string CreditPrice { get; set; }

        [RegularExpression(MyVars.CurrencyRegx, ErrorMessage = "Must be greater than 0 ")]
        public string CashPrice { get; set; }

        [RegularExpression(MyVars.CurrencyRegx, ErrorMessage = "Must be greater than 0 ")]
        public Nullable<int> QTY { get; set; }

        [MaxLength(60, ErrorMessage = "Must Be Less Than 60 Charcters")]
        [Required(ErrorMessage = "Please Enter A Product Name")]
        public string ProductName { get; set; }

        [MaxLength(140, ErrorMessage = "Must Be Less Than 140 Charcters")]
        [MinLength(20, ErrorMessage = "Please Enter A Description")]
        public string Description { get; set; }

    }


    public class MessageMetaData
    {
        [MaxLength(100, ErrorMessage = "Must Be Less Than 100 Charcters")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Please Enter A <essage")]
        public string Body { get; set; }
    }



    public class GoalMetaData
    {
        [Required(ErrorMessage = "Please Enter A Goal Name")]
        [MaxLength(60, ErrorMessage = "Must Be Less Than 60 Charcters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please Enter A Description")]
        //[MaxLength(140, ErrorMessage = "Must Be Less Than 140 Charcters")]
        //[MinLength(20, ErrorMessage = "Please Enter A Better Description")]
        public string Description { get; set; }
        [RegularExpression(MyVars.CurrencyRegx, ErrorMessage = "Must be greater than 0 ")]
        [Required(ErrorMessage = "Please Enter A Number")]
        public string TargetNum { get; set; }
        [RegularExpression(MyVars.CurrencyRegx, ErrorMessage = "Must be greater than 0 ")]
        public string QtyMax { get; set; }

        [Required(ErrorMessage = "Please Enter An Experation Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public System.DateTime ExpDate { get; set; }
    }

    public class FileMetaData
    {
        [Required(ErrorMessage = "Please Enter A File Name")]
        [MaxLength(60, ErrorMessage = "Must Be Less Than 60 Charcters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please Enter A File URL")]
        [DataType(DataType.Url)]
        public string Url { get; set; }

        public string Description { get; set; }
        [DataType(DataType.ImageUrl)]
        public string ThumbnailUrl { get; set; }
    }


    public class CampaignReviewMetaData
    {
        [MaxLength(60, ErrorMessage = "Must Be Less Than 60 Charcters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please Enter A Review")]
        [MinLength(100, ErrorMessage = "Please Enter A Better Review")]
        public string Content { get; set; }
    }


    public class CampaignCommentMetaData
    {
        [MaxLength(60, ErrorMessage = "Must Be Less Than 60 Charcters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please Enter A Comment")]
        public string Content { get; set; }
    }


    public class CampaignMetaData
    {
        [Required(ErrorMessage = "Please Enter A Campaign Name")]
        [MaxLength(60, ErrorMessage = "Must Be Less Than 60 Charcters")]
        public string Name { get; set; }

        public string Description { get; set; }
        public string Email { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public System.DateTime EndDate { get; set; }

        [MaxLength(140, ErrorMessage = "Must Be Less Than 140 Charcters")]
        [MinLength(20, ErrorMessage = "Please Enter A Better Intro")]
        public string Intro { get; set; }
        
        [DataType(DataType.ImageUrl)]
        public string Avatar { get; set; }
    }

    public class AlbumMetaData
    {

        [Required(ErrorMessage = "Please Enter An Album Name")]
        [MaxLength(60, ErrorMessage = "Must Be Less Than 60 Charcters")]
        public string Name { get; set; }
        public string Description { get; set; }
    }


    public class AddressMetaData
    {
        [Required(ErrorMessage = "Please Enter Your Address")]
        public string Add1 { get; set; }
        public string Add2 { get; set; }

        [Required(ErrorMessage = "Please Enter Your City")]
        public string City { get; set; }

        [Required(ErrorMessage = "Please Enter Your State")]
        public string State { get; set; }

        [Required(ErrorMessage = "Please Enter Your Zip Code")]
        public string Zip { get; set; }

        [Required(ErrorMessage = "Please Enter A Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

           [DataType(DataType.PhoneNumber)]
        public string Cell { get; set; }
    }

}