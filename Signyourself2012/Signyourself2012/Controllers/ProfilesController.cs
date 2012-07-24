using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Signyourself2012.Models;
using System.Web.Security;
using WebMatrix.WebData;
using System.Data.Entity.Validation;

namespace Signyourself2012.Controllers
{
    public class ProfilesController : Controller
    {
        private SignYourselfEntities db = new SignYourselfEntities();

        //
        // GET: /Profiles/
        [Authorize]
        public ActionResult Index()
        {
            var profiles = db.Profiles.Include(p => p.User);
            return View(profiles.ToList());
        }

        //
        // GET: /Profiles/Details/5

        public ActionResult Details(string username = "")
        {
            Profile profile = db.Profiles.SingleOrDefault(p => p.User.UserName == username);
            if (profile == null)
            {
                return HttpNotFound();
            }
            if (profile.PrivacyLevelID == 1)
            {
                if (!WebSecurity.IsAuthenticated) { return HttpNotFound(); }
                if (profile.UserId != (Guid)Membership.GetUser().ProviderUserKey) { return HttpNotFound(); }
            }
           
            return View(profile);
        }

      

        //
        // GET: /Profiles/Edit/5
        [Authorize]
        public ActionResult Edit(String username = "")
        {
            if (username == null) return HttpNotFound();
            Profile profile = db.Profiles.SingleOrDefault(p => p.User.UserName == username);
            if (profile == null)
            {
                return HttpNotFound();
            }
            var providerUserKey = Membership.GetUser().ProviderUserKey;
            if (providerUserKey != null && profile.UserId != (Guid)providerUserKey) { return HttpNotFound(); }

           
            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName", profile.UserId);
            return View(profile);
        }

        //
        // POST: /Profiles/Edit/5

        [HttpPost]
        [Authorize]
        public ActionResult Edit(Profile profile)
        {
            if (profile.UserId != (Guid)Membership.GetUser().ProviderUserKey) { return HttpNotFound(); }
            if (ModelState.IsValid)
            {
                db.Entry(profile).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName", profile.UserId);
            return View(profile);
        }


        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}