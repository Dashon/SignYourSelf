using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using SignyourselfHelpers;
using WebMatrix.WebData;
using DotNetOpenAuth.OAuth;
using System.Web.Helpers;
using Microsoft.Web.WebPages.OAuth;


namespace Signyourself2012
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Facebook.Initialize("457516627592275", "2b69bdef91e7d22d38312c83fc39a061", "DefaultConnection");
            
            WebSecurity.InitializeDatabaseConnection("DefaultConnection", "Users", "UserId", "UserName", autoCreateTables: false);

            // To let users of this site log in using their accounts from other sites such as Facebook, Twitter, 
            // and Windows Live, you must update this site. For more information visit 
            // http://go.microsoft.com/fwlink/?LinkID=226949       
   
            WebMail.SmtpServer = "smtp.gmail.com";
            WebMail.EnableSsl = true;
            WebMail.UserName = "dashon.howard@gmail.com";
            WebMail.Password = "Bigbiz2day!!";
            WebMail.From = "Signyourself@gmail.com";
            ModelMetadataProviders.Current = new MetadataProvider();
        }
    }
}