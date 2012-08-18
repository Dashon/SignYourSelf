using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Signyourself2012.Models;
using System.Web.Security;

namespace Signyourself2012.Controllers
{
    public class UserCommentsController : Controller
    {
        private readonly SignYourselfEntities _db = new SignYourselfEntities();

        //
        // GET: /UserComments/
        [Authorize]
        public ActionResult Index()
        {
            var usercomments = _db.UserComments.Include(u => u.Author).Include(u => u.User);
            return View(usercomments.ToList());
        }

        //
        // GET: /UserComments/Details/5

        public ActionResult Details(int id = 0)
        {
            UserComment usercomment = _db.UserComments.Find(id);
            if (usercomment == null)
            {
                return HttpNotFound();
            }
            return View(usercomment);
        }

        //
        // GET: /UserComments/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.AuthorID = new SelectList(_db.Users, "UserId", "UserName");
            ViewBag.ReceiverID = new SelectList(_db.Users, "UserId", "UserName");
            return View();
        }

        //
        // POST: /UserComments/Create

        [HttpPost]
        [Authorize]
        public ActionResult Create(UserComment usercomment)
        {
            if (ModelState.IsValid)
            {
                _db.UserComments.Add(usercomment);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AuthorID = new SelectList(_db.Users, "UserId", "UserName", usercomment.AuthorID);
            ViewBag.ReceiverID = new SelectList(_db.Users, "UserId", "UserName", usercomment.ReceiverID);
            return View(usercomment);
        }

        //
        // GET: /UserComments/Edit/5
        [Authorize]
        public ActionResult Edit(int id = 0)
        {
            UserComment usercomment = _db.UserComments.Find(id);
            if (usercomment.AuthorID != (Guid)Membership.GetUser().ProviderUserKey) { return HttpNotFound(); }
            if (usercomment == null)
            {
                return HttpNotFound();
            }
            ViewBag.AuthorID = new SelectList(_db.Users, "UserId", "UserName", usercomment.AuthorID);
            ViewBag.ReceiverID = new SelectList(_db.Users, "UserId", "UserName", usercomment.ReceiverID);
            return HttpNotFound();
        }

        //
        // POST: /UserComments/Edit/5

        [HttpPost]
        [Authorize]
        public ActionResult Edit(UserComment usercomment)
        {
            if (usercomment.AuthorID != (Guid)Membership.GetUser().ProviderUserKey) { return HttpNotFound(); }
            if (ModelState.IsValid)
            {
                _db.Entry(usercomment).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return HttpNotFound();
        }

        //
        // GET: /UserComments/Delete/5
        [Authorize]
        public ActionResult Delete(int id = 0)
        {
            UserComment usercomment = _db.UserComments.Find(id);
            if (usercomment.AuthorID != (Guid)Membership.GetUser().ProviderUserKey) { return HttpNotFound(); }
            if (usercomment == null)
            {
                return HttpNotFound();
            }
            return View(usercomment);
        }

        //
        // POST: /UserComments/Delete/5

        [HttpPost, ActionName("Delete")]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            UserComment usercomment = _db.UserComments.Find(id);
            if (usercomment.AuthorID != (Guid)Membership.GetUser().ProviderUserKey) { return HttpNotFound(); }
            usercomment.IsDeactivated = true;
            _db.Entry(usercomment).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }
    }
}