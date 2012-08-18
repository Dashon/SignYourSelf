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
    public class MessagesController : Controller
    {
        private readonly SignYourselfEntities _db = new SignYourselfEntities();

        //
        // GET: /Messages/

        public ActionResult Index()
        {
            var messages = _db.Messages.Include(m => m.Reciepient);
            return View(messages.ToList());
        }

        //
        // GET: /Messages/Details/5

        public ActionResult Details(int id = 0)
        {
            Message message = _db.Messages.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            return View(message);
        }

        //
        // GET: /Messages/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.UserID = new SelectList(_db.Users, "UserId", "UserName");
            return View();
        }

        //
        // POST: /Messages/Create

        [HttpPost]
        [Authorize]
        public ActionResult Create(Message message)
        {
            if (ModelState.IsValid)
            {
                _db.Messages.Add(message);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserID = new SelectList(_db.Users, "UserId", "UserName", message.UserID);
            return View(message);
        }

        //
        // GET: /Messages/Edit/5
        [Authorize]
        public ActionResult Edit(int id = 0)
        {
            Message message = _db.Messages.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserID = new SelectList(_db.Users, "UserId", "UserName", message.UserID);
            return HttpNotFound();
        }

        //
        // POST: /Messages/Edit/5

        [HttpPost]
        [Authorize]
        public ActionResult Edit(Message message)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(message).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserID = new SelectList(_db.Users, "UserId", "UserName", message.UserID);
            return HttpNotFound();
        }

        //
        // GET: /Messages/Delete/5
        [Authorize]
        public ActionResult Delete(int id = 0)
        {
            Message message = _db.Messages.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            return View(message);
        }

        //
        // POST: /Messages/Delete/5

        [HttpPost, ActionName("Delete")]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            Message message = _db.Messages.Find(id);
            _db.Messages.Remove(message);
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