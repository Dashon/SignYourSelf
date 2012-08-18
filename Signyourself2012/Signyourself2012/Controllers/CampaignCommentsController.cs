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
        private readonly SignYourselfEntities _db = new SignYourselfEntities();

        //
        // GET: /CampaignComments/
         [Authorize]
        public ActionResult Index()
        {
            var campaigncomments = _db.CampaignComments.Include(c => c.Campaign).Include(c => c.User);
            return View(campaigncomments.ToList());
        }

        //
        // GET: /CampaignComments/Details/5

        public ActionResult Details(int id = 0)
        {
            CampaignComment campaigncomment = _db.CampaignComments.Find(id);
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
            ViewBag.CampaignID = new SelectList(_db.Campaigns, "CampaignID", "Name");
            ViewBag.UserID = new SelectList(_db.Users, "UserId", "UserName");
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
                _db.CampaignComments.Add(campaigncomment);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CampaignID = new SelectList(_db.Campaigns, "CampaignID", "Name", campaigncomment.CampaignID);
            ViewBag.UserID = new SelectList(_db.Users, "UserId", "UserName", campaigncomment.UserID);
            return View(campaigncomment);
        }

        //
        // GET: /CampaignComments/Edit/5
         [Authorize]
        public ActionResult Edit(int id = 0)
        {
            CampaignComment campaigncomment = _db.CampaignComments.Find(id);
            if (campaigncomment == null)
            {
                return HttpNotFound();
            }
            ViewBag.CampaignID = new SelectList(_db.Campaigns, "CampaignID", "Name", campaigncomment.CampaignID);
            ViewBag.UserID = new SelectList(_db.Users, "UserId", "UserName", campaigncomment.UserID);
            return HttpNotFound();
        }

        //
        // POST: /CampaignComments/Edit/5

        [HttpPost]
        [Authorize]
        public ActionResult Edit(CampaignComment campaigncomment)
        {
            if (campaigncomment==null)return HttpNotFound();
            if (ModelState.IsValid)
            {
                _db.Entry(campaigncomment).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CampaignID = new SelectList(_db.Campaigns, "CampaignID", "Name", campaigncomment.CampaignID);
            ViewBag.UserID = new SelectList(_db.Users, "UserId", "UserName", campaigncomment.UserID);
            return View(campaigncomment);
        }

        //
        // GET: /CampaignComments/Delete/5
         [Authorize]
        public ActionResult Delete(int id = 0)
        {
            CampaignComment campaigncomment = _db.CampaignComments.Find(id);
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
            CampaignComment campaigncomment = _db.CampaignComments.Find(id);
            if (campaigncomment.UserID != (Guid)Membership.GetUser().ProviderUserKey) { return HttpNotFound(); }
            campaigncomment.IsDeactivated = true;
            _db.Entry(campaigncomment).State = EntityState.Modified;
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