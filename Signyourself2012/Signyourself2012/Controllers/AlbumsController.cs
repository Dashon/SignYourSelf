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
    public class AlbumsController : Controller
    {
        private SignYourselfEntities db = new SignYourselfEntities();

        //
        // GET: /Albums/

        public ActionResult Index()
        {
            var albums = db.Albums.Include(a => a.AlbumType).Include(a => a.PrivacyLevel).Include(a => a.User).Include(a => a.Campaign);
            return View(albums.ToList());
        }

        //
        // GET: /Albums/Details/5

        public ActionResult Details(int id = 0)
        {
            Album album = db.Albums.Find(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            return View(album);
        }

        //
        // GET: /Albums/Create
         [Authorize]
        public ActionResult Create()
        {
            ViewBag.AlbumTypeID = new SelectList(db.AlbumTypes, "AlbumTypeID", "Name");
            ViewBag.PrivacyLevelId = new SelectList(db.PrivacyLevels, "PrivacyLevelID", "Name");
            ViewBag.UserID = new SelectList(db.Users, "UserId", "UserName");
            ViewBag.CampaignID = new SelectList(db.Campaigns, "CampaignID", "Name");
            return View();
        }

        //
        // POST: /Albums/Create

        [HttpPost]
        [Authorize]
        public ActionResult Create(Album album)
        {
            if (ModelState.IsValid)
            {
                db.Albums.Add(album);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AlbumTypeID = new SelectList(db.AlbumTypes, "AlbumTypeID", "Name", album.AlbumTypeID);
            ViewBag.PrivacyLevelId = new SelectList(db.PrivacyLevels, "PrivacyLevelID", "Name", album.PrivacyLevelId);
            ViewBag.UserID = new SelectList(db.Users, "UserId", "UserName", album.UserID);
            ViewBag.CampaignID = new SelectList(db.Campaigns, "CampaignID", "Name", album.CampaignID);
            return View(album);
        }

        //
        // GET: /Albums/Edit/5
         [Authorize]
        public ActionResult Edit(int id = 0)
        {
            Album album = db.Albums.Find(id);

            if (album.UserID != (Guid)Membership.GetUser().ProviderUserKey) { return HttpNotFound(); }
            if (album == null)
            {
                return HttpNotFound();
            }
            ViewBag.AlbumTypeID = new SelectList(db.AlbumTypes, "AlbumTypeID", "Name", album.AlbumTypeID);
            ViewBag.PrivacyLevelId = new SelectList(db.PrivacyLevels, "PrivacyLevelID", "Name", album.PrivacyLevelId);
            ViewBag.UserID = new SelectList(db.Users, "UserId", "UserName", album.UserID);
            ViewBag.CampaignID = new SelectList(db.Campaigns, "CampaignID", "Name", album.CampaignID);
            return View(album);
        }

        //
        // POST: /Albums/Edit/5

        [HttpPost]
        [Authorize]
        public ActionResult Edit(Album album)
        {
            if (album.UserID != (Guid)Membership.GetUser().ProviderUserKey) { return HttpNotFound(); }
            if (ModelState.IsValid)
            {
                db.Entry(album).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AlbumTypeID = new SelectList(db.AlbumTypes, "AlbumTypeID", "Name", album.AlbumTypeID);
            ViewBag.PrivacyLevelId = new SelectList(db.PrivacyLevels, "PrivacyLevelID", "Name", album.PrivacyLevelId);
            ViewBag.UserID = new SelectList(db.Users, "UserId", "UserName", album.UserID);
            ViewBag.CampaignID = new SelectList(db.Campaigns, "CampaignID", "Name", album.CampaignID);
            return View(album);
        }

        //
        // GET: /Albums/Delete/5
         [Authorize]
        public ActionResult Delete(int id = 0)
        {
            Album album = db.Albums.Find(id);
            if (album.UserID != (Guid)Membership.GetUser().ProviderUserKey) { return HttpNotFound(); }
            if (album == null)
            {
                return HttpNotFound();
            }
            return View(album);
        }

        //
        // POST: /Albums/Delete/5

        [HttpPost, ActionName("Delete")]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            Album album = db.Albums.Find(id);
            if (album.UserID != (Guid)Membership.GetUser().ProviderUserKey) { return HttpNotFound(); }
            album.IsDeactivated = true;
            db.Entry(album).State = EntityState.Modified;
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