﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Signyourself2012.Models;

namespace Signyourself2012.Controllers
{
    public class CampaignUpdatesController : Controller
    {
        private readonly SignYourselfEntities _db = new SignYourselfEntities();

        //
        // GET: /CampaignUpdates/

        public ActionResult Index()
        {
            var campaignupdates = _db.CampaignUpdates.Include(c => c.Campaign).Include(c => c.User);
            return View(campaignupdates.ToList());
        }

        //
        // GET: /CampaignUpdates/Details/5

        public ActionResult Details(int id = 0)
        {
            CampaignUpdate campaignupdate = _db.CampaignUpdates.Find(id);
            if (campaignupdate == null)
            {
                return HttpNotFound();
            }
            return View(campaignupdate);
        }

        //
        // GET: /CampaignUpdates/Create

        public ActionResult Create()
        {
            ViewBag.CampaignID = new SelectList(_db.Campaigns, "CampaignID", "Name");
            ViewBag.UserID = new SelectList(_db.Users, "UserId", "UserName");
            return View();
        }

        //
        // POST: /CampaignUpdates/Create

        [HttpPost]
        public ActionResult Create(CampaignUpdate campaignupdate)
        {
            if (ModelState.IsValid)
            {
                _db.CampaignUpdates.Add(campaignupdate);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CampaignID = new SelectList(_db.Campaigns, "CampaignID", "Name", campaignupdate.CampaignID);
            ViewBag.UserID = new SelectList(_db.Users, "UserId", "UserName", campaignupdate.UserID);
            return View(campaignupdate);
        }

        //
        // GET: /CampaignUpdates/Edit/5

        public ActionResult Edit(int id = 0)
        {
            CampaignUpdate campaignupdate = _db.CampaignUpdates.Find(id);
            if (campaignupdate == null)
            {
                return HttpNotFound();
            }
            ViewBag.CampaignID = new SelectList(_db.Campaigns, "CampaignID", "Name", campaignupdate.CampaignID);
            ViewBag.UserID = new SelectList(_db.Users, "UserId", "UserName", campaignupdate.UserID);
            return View(campaignupdate);
        }

        //
        // POST: /CampaignUpdates/Edit/5

        [HttpPost]
        public ActionResult Edit(CampaignUpdate campaignupdate)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(campaignupdate).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CampaignID = new SelectList(_db.Campaigns, "CampaignID", "Name", campaignupdate.CampaignID);
            ViewBag.UserID = new SelectList(_db.Users, "UserId", "UserName", campaignupdate.UserID);
            return View(campaignupdate);
        }

        //
        // GET: /CampaignUpdates/Delete/5

        public ActionResult Delete(int id = 0)
        {
            CampaignUpdate campaignupdate = _db.CampaignUpdates.Find(id);
            if (campaignupdate == null)
            {
                return HttpNotFound();
            }
            return View(campaignupdate);
        }

        //
        // POST: /CampaignUpdates/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            CampaignUpdate campaignupdate = _db.CampaignUpdates.Find(id);
            _db.CampaignUpdates.Remove(campaignupdate);
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