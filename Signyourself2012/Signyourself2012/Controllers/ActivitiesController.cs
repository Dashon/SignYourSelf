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
    public class ActivitiesController : Controller
    {
        private SignYourselfEntities db = new SignYourselfEntities();

        //
        // GET: /Activities/
        [Authorize]
        public ActionResult Index()
        {
            var activities = db.Activities.Include(a => a.ActivityType).Include(a => a.User);
            return View(activities.ToList());
        }

        //
        // GET: /Activities/Details/5
        [Authorize]
        public ActionResult Details(int id = 0)
        {
            Activity activity = db.Activities.Find(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            return View(activity);
        }        

        //
        // POST: /Activities/Create

        [HttpPost]
        public ActionResult Create(Activity activity)
        {
            if (ModelState.IsValid)
            {
                db.Activities.Add(activity);
                db.SaveChanges();
               return HttpNotFound();
            }

            return HttpNotFound();
        }

        

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}