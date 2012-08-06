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
        private SignYourselfEntities db = new SignYourselfEntities();

        //
        // GET: /Goals/

        public ActionResult Index()
        {
            var goals = db.Goals.Include(g => g.GoalType).Include(g => g.Creator).Include(g => g.Campaign);
            return View(goals.ToList());
        }

        //
        // GET: /Goals/Details/5

        public ActionResult Details(int id = 0)
        {
            Goal goal = db.Goals.Find(id);
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
            Campaign campaign = db.Campaigns.Find(CampaignId);
            if (campaign == null) { return HttpNotFound(); }
            if (campaign.Creator.UserId != (Guid)Membership.GetUser().ProviderUserKey) { return HttpNotFound(); }
            ViewBag.GoalTypeID = new SelectList(db.GoalTypes, "GoalTypeID", "Name");

            ViewBag.CampaignID = CampaignId;
            return View();
        }

        //
        // POST: /Goals/Create

        [HttpPost]
        [Authorize]
        public ActionResult Create(Goal goal)
        {
            Campaign campaign = db.Campaigns.Find(goal.CampaignID);
            if (campaign == null) { return HttpNotFound(); }
            if (campaign.Creator.UserId != (Guid)Membership.GetUser().ProviderUserKey) { return HttpNotFound(); }
            if (ModelState.IsValid)
            {
                try
                {
                    goal.Approved = true;
                    goal.UserID = (Guid)Membership.GetUser().ProviderUserKey;
                    goal.DateCreated = DateTime.Today;
                    goal.Status = "Approved";
                    
                    db.Goals.Add(goal);
                    db.SaveChanges();
                    ViewBag.Message = "Goals Saved";
                    return RedirectToAction("Index");
                }
                catch (EntityException es)
                {

                    ViewBag.GoalTypeID = new SelectList(db.GoalTypes, "GoalTypeID", "Name");
                    ViewBag.Message = es;
                    return View(goal);
                }
            }

            ViewBag.GoalTypeID = new SelectList(db.GoalTypes, "GoalTypeID", "Name");
            return View(goal);

        }

        //
        // GET: /Goals/Edit/5
        [Authorize]
        public ActionResult Edit(int id = 0)
        {
            Goal goal = db.Goals.Find(id);
            Campaign campaign = goal.Campaign;
            if (campaign == null) { return HttpNotFound(); }
            if (campaign.Creator.UserId != (Guid)Membership.GetUser().ProviderUserKey) { return HttpNotFound(); }
            if (goal == null)
            {
                return HttpNotFound();
            }
            ViewBag.GoalTypeID = new SelectList(db.GoalTypes, "GoalTypeID", "Name", goal.GoalTypeID);
            return View(goal);
        }

        //
        // POST: /Goals/Edit/5

        [HttpPost]
        [Authorize]
        public ActionResult Edit(Goal goal)
        {
            Campaign campaign = goal.Campaign;
            if (campaign == null) { return HttpNotFound(); }
            if (campaign.Creator.UserId != (Guid)Membership.GetUser().ProviderUserKey) { return HttpNotFound(); }
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(goal).State = EntityState.Modified;
                    db.SaveChanges();
                    ViewBag.Message = "Goal Saved";
                    return RedirectToAction("Index");
                }
                catch
                {
                    ViewBag.Message = "Goal Not Saved";
                    return View();
                }
            }
            ViewBag.GoalTypeID = new SelectList(db.GoalTypes, "GoalTypeID", "Name", goal.GoalTypeID);
            return View();
        }

        //
        // GET: /Goals/Delete/5
        [Authorize]
        public ActionResult Delete(int id = 0)
        {
            Goal goal = db.Goals.Find(id);
            Campaign campaign = goal.Campaign;
            if (campaign == null) { return HttpNotFound(); }
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
            Goal goal = db.Goals.Find(id);
            Campaign campaign = goal.Campaign;
            if (campaign == null) { return HttpNotFound(); }
            if (campaign.Creator.UserId != (Guid)Membership.GetUser().ProviderUserKey) { return HttpNotFound(); }
            goal.IsDeactivated = true;
            db.Entry(goal).State = EntityState.Modified;
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