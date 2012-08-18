using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Signyourself2012.Models;
using System.Web.Security;
using WebMatrix.WebData;
using System.Data.Entity.Validation;

namespace Signyourself2012.Controllers
{
    public class ProfilesController : Controller
    {
        private readonly SignYourselfEntities _db = new SignYourselfEntities();

        //
        // GET: /Profiles/
        [Authorize]
        public ActionResult Index()
        {
            var profiles = _db.Profiles.Include(p => p.User);
            return View(profiles.ToList());
        }

        //
        // GET: /Profiles/Details/5

        public ActionResult Details(string username = "")
        {
            Profile profile = _db.Profiles.SingleOrDefault(p => p.User.UserName == username);
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
            Profile profile = _db.Profiles.SingleOrDefault(p => p.User.UserName == username);
            if (profile == null)
            {
                return HttpNotFound();
            }
            var providerUserKey = Membership.GetUser().ProviderUserKey;
            if (providerUserKey != null && profile.UserId != (Guid)providerUserKey) { return HttpNotFound(); }
           
            
            ViewBag.UserId = new SelectList(_db.Users, "UserId", "UserName", profile.UserId);
            return View(profile);
        }

        //
        // POST: /Profiles/Edit/5

        [HttpPost]
        [Authorize]
        public ActionResult Edit(Profile profile)
        {
            profile.User = _db.Users.Single(u => u.UserId == (Guid)profile.UserId);
            var providerUserKey = Membership.GetUser().ProviderUserKey;
            if (providerUserKey != null && profile.UserId != (Guid)providerUserKey) { return HttpNotFound(); }
            profile.LastUpdatedDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                _db.Entry(profile).State = EntityState.Modified;
                try
                {
                    _db.SaveChanges();
                    return RedirectToAction("Details", new { username = profile.User.UserName });
                }
                catch (EntityException es)
                {
                    ViewBag.Message = es;
                    return View(profile);
                }
            }
            ViewBag.UserId = new SelectList(_db.Users, "UserId", "UserName", profile.UserId);
            return View(profile);
        }


      
        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }
    }
}