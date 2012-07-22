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
    public class RewardsController : Controller
    {
        private SignYourselfEntities db = new SignYourselfEntities();

        //
        // GET: /Rewards/
         [Authorize]
        public ActionResult Index()
        {
            var rewards = db.Rewards.Include(r => r.RewardType).Include(r => r.Creator).Include(r => r.Goal);
            return View(rewards.ToList());
        }

        //
        // GET: /Rewards/Details/5

        public ActionResult Details(int id = 0)
        {
            Reward reward = db.Rewards.Find(id);
            if (reward == null)
            {
                return HttpNotFound();
            }
            return View(reward);
        }

        //
        // GET: /Rewards/Create
        [Authorize]
        public ActionResult Create(int CampaignId = 5)
        {
            Campaign campaign = db.Campaigns.Find(CampaignId);
            if (campaign == null) { return HttpNotFound(); }
            //if (campaign.Creator.UserId != (Guid)Membership.GetUser().ProviderUserKey) { return HttpNotFound(); }
            ViewBag.RewardTypeID = new SelectList(db.RewardTypes, "RewardTypeID", "Name");
            //ViewBag.UserID = new SelectList(db.Users, "UserId", "UserName");            
            ViewBag.CampaignID = CampaignId;
            ViewBag.GoalID = new SelectList(db.Goals.Where(g => g.CampaignID == CampaignId), "GoalID", "Name");
            return View();
        }

        //
        // POST: /Rewards/Create

        [HttpPost]
        [Authorize]
        public ActionResult Create(Reward reward)
        {
            Campaign campaign = db.Campaigns.Find(reward.Goal.Campaign.CampaignID);
            if (campaign == null) { return HttpNotFound(); }
            if (campaign.Creator.UserId != (Guid)Membership.GetUser().ProviderUserKey) { return HttpNotFound(); }
            if (ModelState.IsValid)
            {
                db.Rewards.Add(reward);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RewardTypeID = new SelectList(db.RewardTypes, "RewardTypeID", "Name", reward.RewardTypeID);
            ViewBag.GoalID = new SelectList(db.Goals.Where(g => g.CampaignID == reward.Goal.CampaignID), "GoalID", "Name", reward.GoalID);
            return View(reward);
        }

        //
        // GET: /Rewards/Edit/5
         [Authorize]
        public ActionResult Edit(int id = 0)
        {
            Reward reward = db.Rewards.Find(id);
            Campaign campaign = db.Campaigns.Find(reward.Goal.CampaignID);
            if (campaign == null) { return HttpNotFound(); }
            if (campaign.Creator.UserId != (Guid)Membership.GetUser().ProviderUserKey) { return HttpNotFound(); }

            if (reward == null)
            {
                return HttpNotFound();
            }
            ViewBag.RewardTypeID = new SelectList(db.RewardTypes, "RewardTypeID", "Name", reward.RewardTypeID);
            ViewBag.GoalID = new SelectList(db.Goals.Where(g => g.CampaignID == reward.Goal.CampaignID), "GoalID", "Name", reward.GoalID);
            return View(reward);
        }

        //
        // POST: /Rewards/Edit/5

        [HttpPost]
        [Authorize]
        public ActionResult Edit(Reward reward)
        {
            Campaign campaign = db.Campaigns.Find(reward.Goal.CampaignID);
            if (campaign == null) { return HttpNotFound(); }
            if (campaign.Creator.UserId != (Guid)Membership.GetUser().ProviderUserKey) { return HttpNotFound(); }
            if (ModelState.IsValid)
            {
                db.Entry(reward).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RewardTypeID = new SelectList(db.RewardTypes, "RewardTypeID", "Name", reward.RewardTypeID);
            ViewBag.GoalID = new SelectList(db.Goals.Where(g => g.CampaignID == reward.Goal.CampaignID), "GoalID", "Name", reward.GoalID);
            return View(reward);
        }

        //
        // GET: /Rewards/Delete/5
         [Authorize]
        public ActionResult Delete(int id = 0)
        {
            Reward reward = db.Rewards.Find(id);
            Campaign campaign = db.Campaigns.Find(reward.Goal.CampaignID);
            if (campaign == null) { return HttpNotFound(); }
            if (campaign.Creator.UserId != (Guid)Membership.GetUser().ProviderUserKey) { return HttpNotFound(); }
            if (reward == null)
            {
                return HttpNotFound();
            }
            return View(reward);
        }

        //
        // POST: /Rewards/Delete/5

        [HttpPost, ActionName("Delete")]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            Reward reward = db.Rewards.Find(id);
            Campaign campaign = db.Campaigns.Find(reward.Goal.Campaign.CampaignID);
            if (campaign == null) { return HttpNotFound(); }
            if (campaign.Creator.UserId != (Guid)Membership.GetUser().ProviderUserKey) { return HttpNotFound(); }
            reward.IsDeactivated = true;
            db.Entry(reward).State = EntityState.Modified;
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