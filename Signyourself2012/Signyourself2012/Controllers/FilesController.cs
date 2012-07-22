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
    public class FilesController : Controller
    {
        private SignYourselfEntities db = new SignYourselfEntities();

        //
        // GET: /Files/
        [Authorize]
        public ActionResult Index()
        {
            var files = db.Files.Include(f => f.LinkSource).Include(f => f.User).Include(f => f.FileType);
            return View(files.ToList());
        }

        //
        // GET: /Files/Details/5

        public ActionResult Details(int id = 0)
        {
            File file = db.Files.Find(id);
            if (file == null)
            {
                return HttpNotFound();
            }
            return View(file);
        }

        //
        // GET: /Files/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.LinkSourceId = new SelectList(db.LinkSources, "LinkTypeID", "WebsiteName");
            ViewBag.FileTypeId = new SelectList(db.FileTypes, "aFileTypeID", "Name");
            return View();
        }

        //
        // POST: /Files/Create

        [HttpPost]
        [Authorize]
        [AcceptVerbs(HttpVerbs.Post)] 
        public ActionResult Create(File file, IEnumerable<HttpPostedFileBase> images)
        {
            if (ModelState.IsValid)
            {
                string physicalPath = HttpContext.Server.MapPath("../") + "UploadImages" + "\\";
                var fileUrl = "";
                if (Request.Files.Count >= 1)
                {
                    for (int i = 0; i < Request.Files.Count; i++)
                    {
                        fileUrl = physicalPath + System.IO.Path.GetFileName(Request.Files[i].FileName);
                        Request.Files[0].SaveAs(fileUrl);
                    }


                    if (fileUrl == "") return View(file);
                    
                    db.Files.Add(file);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            ViewBag.LinkSourceId = new SelectList(db.Albums.Where(a => a.UserID==(Guid)Membership.GetUser().ProviderUserKey), "AlbumID", "Name");
            ViewBag.FileTypeId = new SelectList(db.FileTypes, "FileTypeID", "Name", file.FileTypeId);
            return View(file);
        }

        //
        // GET: /Files/Edit/5
        [Authorize]
        public ActionResult Edit(int id = 0)
        {
            File file = db.Files.Find(id);

            if (file.UserID != (Guid)Membership.GetUser().ProviderUserKey) { return HttpNotFound(); }
            if (file == null)
            {
                return HttpNotFound();
            }
            ViewBag.LinkSourceId = new SelectList(db.LinkSources, "LinkTypeID", "WebsiteName", file.LinkSourceId);
            ViewBag.UserID = new SelectList(db.Users, "UserId", "UserName", file.UserID);
            ViewBag.FileTypeId = new SelectList(db.FileTypes, "FileTypeID", "Name", file.FileTypeId);
            return View(file);
        }

        //
        // POST: /Files/Edit/5

        [HttpPost]
        [Authorize]
        public ActionResult Edit(File file)
        {
            if (file.UserID != (Guid)Membership.GetUser().ProviderUserKey) { return HttpNotFound(); }
            if (ModelState.IsValid)
            {
                db.Entry(file).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LinkSourceId = new SelectList(db.LinkSources, "LinkTypeID", "WebsiteName", file.LinkSourceId);
            ViewBag.FileTypeId = new SelectList(db.FileTypes, "FileTypeID", "Name", file.FileTypeId);
            return View(file);
        }

        //
        // GET: /Files/Delete/5
        [Authorize]
        public ActionResult Delete(int id = 0)
        {
            File file = db.Files.Find(id);

            if (file.UserID != (Guid)Membership.GetUser().ProviderUserKey) { return HttpNotFound(); }
            if (file == null)
            {
                return HttpNotFound();
            }
            return View(file);
        }

        //
        // POST: /Files/Delete/5

        [HttpPost, ActionName("Delete")]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            File file = db.Files.Find(id);
            if (file.UserID != (Guid)Membership.GetUser().ProviderUserKey) { return HttpNotFound(); }
            file.IsDeactivated = true;
            db.Entry(file).State = EntityState.Modified;
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