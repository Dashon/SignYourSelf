using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Signyourself2012.Models;
using WebMatrix.WebData;
using System.Web.Security;

namespace Signyourself2012.Controllers
{
    public class CampaignsController : Controller
    {
        private readonly SignYourselfEntities _db = new SignYourselfEntities();
        //
        // GET: /Campaigns/
        [Authorize]
        public ActionResult Index()
        {
            var campaigns = _db.Campaigns.Include(c => c.Account).Include(c => c.CampaignType).Include(c => c.Genre).Include(c => c.Creator);
            return View(campaigns.ToList());
        }

        
        // Get /Admin/
        [Authorize]
        public ActionResult Admin()
        {
            if (!Roles.IsUserInRole("SiteAdmin")) return HttpNotFound();
            var campaigns = _db.Campaigns.Include(c => c.Account).Include(c => c.CampaignType).Include(c => c.Genre).Include(c => c.Creator);
            return View(campaigns.ToList());
        }

        //
        // GET: /Campaigns/Details/5

        public ActionResult Details(int id = 0)
        {

            Campaign campaign = _db.Campaigns.Find(id);

            if (campaign == null) return HttpNotFound();

            if (campaign.CampaignPrivacyID == 1)
            {
                if (!WebSecurity.IsAuthenticated) { return HttpNotFound(); }
                var currentUserId = (Guid)Membership.GetUser().ProviderUserKey;
                if (campaign.Creator.UserId != currentUserId) { return HttpNotFound(); }
            }

            return View(campaign);
        }

        //
        // GET: /Campaigns/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.CampaignTypeId = new SelectList(_db.CampaignTypes, "CampaignTypeID", "Name");
            ViewBag.GenreID = new SelectList(_db.Genres, "ID", "Name");

            return View();
        }

        //
        // POST: /Campaigns/Create

        [HttpPost]
        [Authorize]
        public ActionResult Create(Campaign campaign)
        {
            
            var currentUserId = (Guid)Membership.GetUser().ProviderUserKey;
            campaign.DateCreated = DateTime.Now;
            campaign.UserId = currentUserId;
            campaign.Appoved = true; //default false, approve by admin
            campaign.Active = true; //default false , activate after submit
            campaign.Email = _db.Users.SingleOrDefault(u => u.UserId == currentUserId).Membership.Email;
            campaign.Location = _db.Users.SingleOrDefault(u => u.UserId == currentUserId).Profile.Location;
            campaign.EndDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                _db.Campaigns.Add(campaign);
                _db.SaveChanges();
                return RedirectToAction("Edit", "Campaigns", new { id = campaign.CampaignID });

            }

            ViewBag.AccountId = new SelectList(_db.Accounts, "AccountID", "AccountID", campaign.AccountId);
            ViewBag.CampaignTypeId = new SelectList(_db.CampaignTypes, "CampaignTypeID", "Name", campaign.CampaignTypeId);
            ViewBag.GenreID = new SelectList(_db.Genres, "ID", "Name", campaign.GenreID);
            ViewBag.UserId = new SelectList(_db.Users, "UserId", "UserName", campaign.UserId);
            return View(campaign);
        }

        //
        // GET: /Campaigns/Edit/5
        [Authorize]
        public ActionResult Edit(int id = 0)
        {
            var currentUserId = (Guid)Membership.GetUser().ProviderUserKey;
            Campaign campaign = _db.Campaigns.Find(id);
            if (campaign == null)return HttpNotFound();
            if (campaign.Creator.UserId != currentUserId) { return HttpNotFound(); }


            ViewBag.AccountId = new SelectList(_db.Accounts, "AccountID", "AccountID", campaign.AccountId);
            ViewBag.CampaignTypeId = new SelectList(_db.CampaignTypes, "CampaignTypeID", "Name", campaign.CampaignTypeId);
            ViewBag.GenreID = new SelectList(_db.Genres, "ID", "Name", campaign.GenreID);
            ViewBag.UserId = new SelectList(_db.Users, "UserId", "UserName", campaign.UserId);
            return View(campaign);
        }

        //
        // POST: /Campaigns/Edit/5

        [HttpPost]
        [Authorize]
        public ActionResult Edit(Campaign campaign)
        {
            if (campaign == null)return HttpNotFound();

            var currentUserId = (Guid)Membership.GetUser().ProviderUserKey;
            var existingCampaign = _db.Campaigns.Find(campaign.CampaignID);
            if (existingCampaign.UserId != currentUserId) { return HttpNotFound(); }

            if (ModelState.IsValid)
            {
                existingCampaign.Location = campaign.Location;
                existingCampaign.Name = campaign.Name;
                existingCampaign.Location = campaign.Location;
                existingCampaign.Intro = campaign.Intro;
                existingCampaign.Email = campaign.Email;
                existingCampaign.EndDate = campaign.EndDate;
                existingCampaign.CampaignTypeId = campaign.CampaignTypeId;
                existingCampaign.GenreID = campaign.GenreID;

                _db.Entry(existingCampaign).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Details", "Campaigns", new { id = existingCampaign.CampaignID });

            }
            ViewBag.AccountId = new SelectList(_db.Accounts, "AccountID", "AccountID", campaign.AccountId);
            ViewBag.CampaignTypeId = new SelectList(_db.CampaignTypes, "CampaignTypeID", "Name", campaign.CampaignTypeId);
            ViewBag.GenreID = new SelectList(_db.Genres, "ID", "Name", campaign.GenreID);
            ViewBag.UserId = new SelectList(_db.Users, "UserId", "UserName", campaign.UserId);
            return View(campaign);
        }

        //
        // GET: /Campaigns/Delete/5
        [Authorize]
        public ActionResult Delete(int id = 0)
        {
            Campaign campaign = _db.Campaigns.Find(id);

            var currentUserId = (Guid)Membership.GetUser().ProviderUserKey;
            if (campaign == null)return HttpNotFound();
            if (campaign.Creator.UserId != currentUserId) { return HttpNotFound(); }
            return View(campaign);
        }

        //
        // POST: /Campaigns/Delete/5

        [HttpPost, ActionName("Delete")]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            var currentUserId = (Guid)Membership.GetUser().ProviderUserKey;
            Campaign campaign = _db.Campaigns.Find(id);
            if (campaign == null)return HttpNotFound();
            if (campaign.Creator.UserId != currentUserId) { return HttpNotFound(); }

            campaign.IsDeactivated = true;
            _db.Entry(campaign).State = EntityState.Modified;
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