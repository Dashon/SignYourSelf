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
    public class CampaignCommentsController : Controller
    {
        private SignYourselfEntities db = new SignYourselfEntities();

        //
        // GET: /CampaignComments/
         [Authorize]
        public ActionResult Index()
        {
            var campaigncomments = db.CampaignComments.Include(c => c.Campaign).Include(c => c.User);
            return View(campaigncomments.ToList());
        }

        //
        // GET: /CampaignComments/Details/5

        public ActionResult Details(int id = 0)
        {
            CampaignComment campaigncomment = db.CampaignComments.Find(id);
            if (campaigncomment == null)
            {
                return HttpNotFound();
            }
            return View(campaigncomment);
        }

        //
        // GET: /CampaignComments/Create
         [Authorize]
        public ActionResult Create()
        {
            ViewBag.CampaignID = new SelectList(db.Campaigns, "CampaignID", "Name");
            ViewBag.UserID = new SelectList(db.Users, "UserId", "UserName");
            return View();
        }

        //
        // POST: /CampaignComments/Create

        [HttpPost]
        [Authorize]
        public ActionResult Create(CampaignComment campaigncomment)
        {
            if (ModelState.IsValid)
            {
                db.CampaignComments.Add(campaigncomment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CampaignID = new SelectList(db.Campaigns, "CampaignID", "Name", campaigncomment.CampaignID);
            ViewBag.UserID = new SelectList(db.Users, "UserId", "UserName", campaigncomment.UserID);
            return View(campaigncomment);
        }

        //
        // GET: /CampaignComments/Edit/5
         [Authorize]
        public ActionResult Edit(int id = 0)
        {
            CampaignComment campaigncomment = db.CampaignComments.Find(id);
            if (campaigncomment == null)
            {
                return HttpNotFound();
            }
            ViewBag.CampaignID = new SelectList(db.Campaigns, "CampaignID", "Name", campaigncomment.CampaignID);
            ViewBag.UserID = new SelectList(db.Users, "UserId", "UserName", campaigncomment.UserID);
            return HttpNotFound();
        }

        //
        // POST: /CampaignComments/Edit/5

        [HttpPost]
        [Authorize]
        public ActionResult Edit(CampaignComment campaigncomment)
        {
            return HttpNotFound();
            if (ModelState.IsValid)
            {
                db.Entry(campaigncomment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CampaignID = new SelectList(db.Campaigns, "CampaignID", "Name", campaigncomment.CampaignID);
            ViewBag.UserID = new SelectList(db.Users, "UserId", "UserName", campaigncomment.UserID);
            return View(campaigncomment);
        }

        //
        // GET: /CampaignComments/Delete/5
         [Authorize]
        public ActionResult Delete(int id = 0)
        {
            CampaignComment campaigncomment = db.CampaignComments.Find(id);
            if (campaigncomment.UserID != (Guid)Membership.GetUser().ProviderUserKey) { return HttpNotFound(); }
            if (campaigncomment == null)
            {
                return HttpNotFound();
            }
            return View(campaigncomment);
        }

        //
        // POST: /CampaignComments/Delete/5

        [HttpPost, ActionName("Delete")]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            CampaignComment campaigncomment = db.CampaignComments.Find(id);
            if (campaigncomment.UserID != (Guid)Membership.GetUser().ProviderUserKey) { return HttpNotFound(); }
            campaigncomment.IsDeactivated = true;
            db.Entry(campaigncomment).State = EntityState.Modified;
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