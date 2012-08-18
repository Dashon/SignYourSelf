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
    public class GoalsController : Controller
    {
        private readonly SignYourselfEntities _db = new SignYourselfEntities();

        //
        // GET: /Goals/

        public ActionResult Index()
        {
            var goals = _db.Goals.Include(g => g.GoalType).Include(g => g.Creator).Include(g => g.Campaign);
            return View(goals.ToList());
        }

        //
        // GET: /Goals/Details/5

        public ActionResult Details(int id = 0)
        {
            Goal goal = _db.Goals.Find(id);
            if (goal == null)
            {
                return HttpNotFound();
            }
            return View(goal);
        }

        //
        // GET: /Goals/Create
        [Authorize]
        public ActionResult Create(int CampaignId)
        {
            var currentUserId = (Guid)Membership.GetUser().ProviderUserKey;
            Campaign campaign = _db.Campaigns.Find(CampaignId);
            if (campaign == null) { return HttpNotFound(); }
            if (campaign.Creator.UserId != currentUserId) { return HttpNotFound(); }
            ViewBag.GoalTypeID = new SelectList(_db.GoalTypes, "GoalTypeID", "Name");

            ViewBag.CampaignID = CampaignId;
            return View();
        }

        //
        // POST: /Goals/Create

        [HttpPost]
        [Authorize]
        public ActionResult Create_Add(Goal goal)
        {
            var currentUserId = (Guid)Membership.GetUser().ProviderUserKey;
            Campaign campaign = _db.Campaigns.Find(goal.CampaignID);
            if (campaign == null)return HttpNotFound();
            if (campaign.Creator.UserId != currentUserId)return HttpNotFound();
           
            if (ModelState.IsValid)
            {
                try
                {
                    goal.Approved = true;
                    goal.UserID = currentUserId;
                    goal.DateCreated = DateTime.Today;
                    goal.Status = "Approved";
                    goal.IsDeactivated = false;
                    goal.CurrentQTY = "0";
                    _db.Goals.Add(goal);
                    _db.SaveChanges();
                    ViewBag.Message = "Goals Saved";
                    return PartialView("_CampaignGoals",campaign.Goals);
                }
                catch (EntityException es)
                {

                    ViewBag.GoalTypeID = new SelectList(_db.GoalTypes, "GoalTypeID", "Name");
                    ViewBag.Message = es;
                    return PartialView("_CampaignGoals", campaign.Goals);
                }
            }

            ViewBag.GoalTypeID = new SelectList(_db.GoalTypes, "GoalTypeID", "Name");
            return PartialView("_CampaignGoals", campaign.Goals);

        }

        //
        // GET: /Goals/Edit/5
        [Authorize]
        public ActionResult Edit(int id = 0)
        {
            var currentUserId = (Guid)Membership.GetUser().ProviderUserKey;
            Goal goal = _db.Goals.Find(id);
            if (goal == null) return HttpNotFound();

            Campaign campaign = goal.Campaign;
            if (campaign == null)return HttpNotFound();
            if (campaign.Creator.UserId != currentUserId) { return HttpNotFound(); }
            
            ViewBag.GoalTypeID = new SelectList(_db.GoalTypes, "GoalTypeID", "Name", goal.GoalTypeID);
            return View(goal);
        }

        //
        // POST: /Goals/Edit/5

        [HttpPost]
        [Authorize]
        public ActionResult Edit(Goal goal)
        {
            if (goal == null)return HttpNotFound();
            var currentUserId = (Guid)Membership.GetUser().ProviderUserKey;
            var existingGoal = _db.Goals.Find(goal.GoalID);
            if (existingGoal.UserID != currentUserId)return HttpNotFound();

            Campaign campaign = existingGoal.Campaign;
            if (campaign == null)return HttpNotFound();
            if (campaign.Creator.UserId != (Guid)Membership.GetUser().ProviderUserKey) { return HttpNotFound(); }
            if (ModelState.IsValid)
            {
                try
                {
                    existingGoal.GoalTypeID = goal.GoalTypeID;
                    existingGoal.Name = goal.Name;
                    existingGoal.Private = goal.Private;
                    existingGoal.QtyMax = goal.QtyMax;
                    existingGoal.TargetNum = goal.TargetNum;

                    _db.Entry(existingGoal).State = EntityState.Modified;
                    _db.SaveChanges();
                    ViewBag.Message = "Goal Saved";
                    return RedirectToAction("Index");
                }
                catch
                {
                    ViewBag.Message = "Goal Not Saved";
                    return View();
                }
            }
            ViewBag.GoalTypeID = new SelectList(_db.GoalTypes, "GoalTypeID", "Name", goal.GoalTypeID);
            return View();
        }

        //
        // GET: /Goals/Delete/5
        [Authorize]
        public ActionResult Delete(int id = 0)
        {
            Goal goal = _db.Goals.Find(id);
            Campaign campaign = goal.Campaign;
            if (campaign == null)return HttpNotFound();
            if (campaign.Creator.UserId != (Guid)Membership.GetUser().ProviderUserKey) { return HttpNotFound(); }
            if (goal == null)
            {
                return HttpNotFound();
            }
            return View(goal);
        }

        //
        // POST: /Goals/Delete/5

        [HttpPost, ActionName("Delete")]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            Goal goal = _db.Goals.Find(id);
            Campaign campaign = goal.Campaign;
            if (campaign == null)return HttpNotFound();
            if (campaign.Creator.UserId != (Guid)Membership.GetUser().ProviderUserKey) { return HttpNotFound(); }
            goal.IsDeactivated = true;
            _db.Entry(goal).State = EntityState.Modified;
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