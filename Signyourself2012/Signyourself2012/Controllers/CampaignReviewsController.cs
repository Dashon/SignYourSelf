using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Signyourself2012.Models;

namespace Signyourself2012.Controllers
{
    public class CampaignReviewsController : Controller
    {
        private SignYourselfEntities db = new SignYourselfEntities();

        //
        // GET: /CampaignReviews/

        public ActionResult Index()
        {
            var campaignreviews = db.CampaignReviews.Include(c => c.Campaign).Include(c => c.User);
            return View(campaignreviews.ToList());
        }

        //
        // GET: /CampaignReviews/Details/5

        public ActionResult Details(int id = 0)
        {
            CampaignReview campaignreview = db.CampaignReviews.Find(id);
            if (campaignreview == null)
            {
                return HttpNotFound();
            }
            return View(campaignreview);
        }

        //
        // GET: /CampaignReviews/Create

        public ActionResult Create()
        {
            ViewBag.CampaignID = new SelectList(db.Campaigns, "CampaignID", "Name");
            ViewBag.UserID = new SelectList(db.Users, "UserId", "UserName");
            return View();
        }

        //
        // POST: /CampaignReviews/Create

        [HttpPost]
        public ActionResult Create(CampaignReview campaignreview)
        {
            if (ModelState.IsValid)
            {
                db.CampaignReviews.Add(campaignreview);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CampaignID = new SelectList(db.Campaigns, "CampaignID", "Name", campaignreview.CampaignID);
            ViewBag.UserID = new SelectList(db.Users, "UserId", "UserName", campaignreview.UserID);
            return View(campaignreview);
        }

        //
        // GET: /CampaignReviews/Edit/5

        public ActionResult Edit(int id = 0)
        {
            CampaignReview campaignreview = db.CampaignReviews.Find(id);
            if (campaignreview == null)
            {
                return HttpNotFound();
            }
            ViewBag.CampaignID = new SelectList(db.Campaigns, "CampaignID", "Name", campaignreview.CampaignID);
            ViewBag.UserID = new SelectList(db.Users, "UserId", "UserName", campaignreview.UserID);
            return View(campaignreview);
        }

        //
        // POST: /CampaignReviews/Edit/5

        [HttpPost]
        public ActionResult Edit(CampaignReview campaignreview)
        {
            if (ModelState.IsValid)
            {
                db.Entry(campaignreview).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CampaignID = new SelectList(db.Campaigns, "CampaignID", "Name", campaignreview.CampaignID);
            ViewBag.UserID = new SelectList(db.Users, "UserId", "UserName", campaignreview.UserID);
            return View(campaignreview);
        }

        //
        // GET: /CampaignReviews/Delete/5

        public ActionResult Delete(int id = 0)
        {
            CampaignReview campaignreview = db.CampaignReviews.Find(id);
            if (campaignreview == null)
            {
                return HttpNotFound();
            }
            return View(campaignreview);
        }

        //
        // POST: /CampaignReviews/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            CampaignReview campaignreview = db.CampaignReviews.Find(id);
            campaignreview.IsDeactivated = true;
            db.Entry(campaignreview).State = EntityState.Modified;
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