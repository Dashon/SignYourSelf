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
            Profile profile = db.Profiles.Single(Profile => Profile.User.UserName == username);
            if (profile.PrivacyLevelID == 1)
            {
                if (!WebSecurity.IsAuthenticated) { return HttpNotFound(); }
                if (profile.UserId != (Guid)Membership.GetUser().ProviderUserKey) { return HttpNotFound(); }
            }
            if (profile == null)
            {
                return HttpNotFound();
            }
            return View(profile);
        }

        //
        // GET: /Profiles/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName");
            return View();
        }

        //
        // POST: /Profiles/Create

        [HttpPost]
        [Authorize]
        public ActionResult Create(Profile profile)
        {
            if (ModelState.IsValid)
            {
                profile.UserId = Guid.NewGuid();
                db.Profiles.Add(profile);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName", profile.UserId);
            return View(profile);
        }

        //
        // GET: /Profiles/Edit/5
        [Authorize]
        public ActionResult Edit(String username = null)
        {
            if (username == null) return HttpNotFound();
            Profile profile = db.Profiles.Single(Profile => Profile.User.UserName == username);
            if (profile.UserId != (Guid)Membership.GetUser().ProviderUserKey) { return HttpNotFound(); }

            if (profile == null)
            {
                return HttpNotFound();
            }
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

        //
        // GET: /Profiles/Delete/5
        [Authorize]
        public ActionResult Delete(String username = null)
        {
            if (username == null) return HttpNotFound();
            Profile profile = db.Profiles.Single(Profile => Profile.User.UserName == username);
            if (profile.UserId != (Guid)Membership.GetUser().ProviderUserKey) { return HttpNotFound(); }

            if (profile == null)
            {
                return HttpNotFound();
            }
            return View(profile);
        }

        //
        // POST: /Profiles/Delete/5

        [HttpPost, ActionName("Delete")]
        [Authorize]
        public ActionResult DeleteConfirmed(string username)
        {
            Profile profile = db.Profiles.Single(Profile => Profile.User.UserName == username);
            if (profile.UserId != (Guid)Membership.GetUser().ProviderUserKey) { return HttpNotFound(); }
            profile.IsDeactivated = true;
            db.Entry(profile).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}