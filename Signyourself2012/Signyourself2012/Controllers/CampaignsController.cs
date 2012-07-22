using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Signyourself2012.Models;
using System.Data.Entity.Validation;
using System.Diagnostics;
using WebMatrix.WebData;
using System.Web.Security;

namespace Signyourself2012.Controllers
{
    public class CampaignsController : Controller
    {
        private SignYourselfEntities db = new SignYourselfEntities();

        //
        // GET: /Campaigns/
         [Authorize]
        public ActionResult Index()
        {
            var campaigns = db.Campaigns.Include(c => c.Account).Include(c => c.CampaignType).Include(c => c.Genre).Include(c => c.Creator);
            return View(campaigns.ToList());
        }

        //
        // GET: /Campaigns/Details/5

        public ActionResult Details(int id = 0)
        {
            Campaign campaign = db.Campaigns.Find(id);
            if (campaign.CampaignPrivacyID == 1)
            {
                if (!WebSecurity.IsAuthenticated) { return HttpNotFound(); }
                if (campaign.Creator.UserId != (Guid)Membership.GetUser().ProviderUserKey) { return HttpNotFound(); }
            }
            if (campaign == null)
            {
                return HttpNotFound();
            }
            return View(campaign);
        }

        //
        // GET: /Campaigns/Create
         [Authorize]
        public ActionResult Create()
        {   ViewBag.CampaignTypeId = new SelectList(db.CampaignTypes, "CampaignTypeID", "Name");
            ViewBag.GenreID = new SelectList(db.Genres, "ID", "Name");            

            ViewBag.GoalTypeID = new SelectList(db.GoalTypes, "GoalTypeID", "Name");
            ViewBag.RewardTypeID = new SelectList(db.RewardTypes, "RewardTypeID", "Name");
           
            return View();
        }

        //
        // POST: /Campaigns/Create

        [HttpPost]
        [Authorize]
        public ActionResult Create(Campaign campaign)
        {
            if (ModelState.IsValid)
            {
                db.Campaigns.Add(campaign);
                try
                {
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (DbEntityValidationException dbEx)
                {
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                        }
                    }
                }
               
            }

            ViewBag.AccountId = new SelectList(db.Accounts, "AccountID", "AccountID", campaign.AccountId);
            ViewBag.CampaignTypeId = new SelectList(db.CampaignTypes, "CampaignTypeID", "Name", campaign.CampaignTypeId);
            ViewBag.GenreID = new SelectList(db.Genres, "ID", "Name", campaign.GenreID);
            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName", campaign.UserId);
            return View(campaign);
        }

        //
        // GET: /Campaigns/Edit/5
         [Authorize]
        public ActionResult Edit(int id = 0)
        {
            Campaign campaign = db.Campaigns.Find(id);
            if (campaign == null) { return HttpNotFound(); }
            if (campaign.Creator.UserId != (Guid)Membership.GetUser().ProviderUserKey) { return HttpNotFound(); }

            if (campaign == null)
            {
                return HttpNotFound();
            }
            ViewBag.AccountId = new SelectList(db.Accounts, "AccountID", "AccountID", campaign.AccountId);
            ViewBag.CampaignTypeId = new SelectList(db.CampaignTypes, "CampaignTypeID", "Name", campaign.CampaignTypeId);
            ViewBag.GenreID = new SelectList(db.Genres, "ID", "Name", campaign.GenreID);
            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName", campaign.UserId);
            return View(campaign);
        }

        //
        // POST: /Campaigns/Edit/5

        [HttpPost]
        [Authorize]
        public ActionResult Edit(Campaign campaign)
        {
            if (campaign == null) { return HttpNotFound(); }
            if (campaign.Creator.UserId != (Guid)Membership.GetUser().ProviderUserKey) { return HttpNotFound(); }

            if (ModelState.IsValid)
            {
                db.Entry(campaign).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AccountId = new SelectList(db.Accounts, "AccountID", "AccountID", campaign.AccountId);
            ViewBag.CampaignTypeId = new SelectList(db.CampaignTypes, "CampaignTypeID", "Name", campaign.CampaignTypeId);
            ViewBag.GenreID = new SelectList(db.Genres, "ID", "Name", campaign.GenreID);
            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName", campaign.UserId);
            return View(campaign);
        }

        //
        // GET: /Campaigns/Delete/5
         [Authorize]
        public ActionResult Delete(int id = 0)
        {
            Campaign campaign = db.Campaigns.Find(id);
            if (campaign == null) { return HttpNotFound(); }
            if (campaign.Creator.UserId != (Guid)Membership.GetUser().ProviderUserKey) { return HttpNotFound(); }

            if (campaign == null)
            {
                return HttpNotFound();
            }
            return View(campaign);
        }

        //
        // POST: /Campaigns/Delete/5

        [HttpPost, ActionName("Delete")]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            Campaign campaign = db.Campaigns.Find(id);
            if (campaign == null) { return HttpNotFound(); }
            if (campaign.Creator.UserId != (Guid)Membership.GetUser().ProviderUserKey) { return HttpNotFound(); }

            campaign.IsDeactivated = true;
            db.Entry(campaign).State = EntityState.Modified;
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