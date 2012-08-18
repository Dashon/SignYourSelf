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
        private readonly SignYourselfEntities _db = new SignYourselfEntities();

        //
        // GET: /Files/
        [Authorize]
        public ActionResult Index()
        {
            var files = _db.Files.Include(f => f.LinkSource).Include(f => f.User).Include(f => f.FileType);
            return View(files.ToList());
        }

        //
        // GET: /Files/Details/5

        public ActionResult Details(int id = 0)
        {
            File file = _db.Files.Find(id);
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
            ViewBag.LinkSourceId = new SelectList(_db.LinkSources, "LinkTypeID", "WebsiteName");
            ViewBag.FileTypeId = new SelectList(_db.FileTypes, "aFileTypeID", "Name");
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
                    
                    _db.Files.Add(file);
                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            ViewBag.LinkSourceId = new SelectList(_db.Albums.Where(a => a.UserID==(Guid)Membership.GetUser().ProviderUserKey), "AlbumID", "Name");
            ViewBag.FileTypeId = new SelectList(_db.FileTypes, "FileTypeID", "Name", file.FileTypeId);
            return View(file);
        }

        //
        // GET: /Files/Edit/5
        [Authorize]
        public ActionResult Edit(int id = 0)
        {
            File file = _db.Files.Find(id);

            if (file.UserID != (Guid)Membership.GetUser().ProviderUserKey) { return HttpNotFound(); }
            if (file == null)
            {
                return HttpNotFound();
            }
            ViewBag.LinkSourceId = new SelectList(_db.LinkSources, "LinkTypeID", "WebsiteName", file.LinkSourceId);
            ViewBag.UserID = new SelectList(_db.Users, "UserId", "UserName", file.UserID);
            ViewBag.FileTypeId = new SelectList(_db.FileTypes, "FileTypeID", "Name", file.FileTypeId);
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
                _db.Entry(file).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LinkSourceId = new SelectList(_db.LinkSources, "LinkTypeID", "WebsiteName", file.LinkSourceId);
            ViewBag.FileTypeId = new SelectList(_db.FileTypes, "FileTypeID", "Name", file.FileTypeId);
            return View(file);
        }

        //
        // GET: /Files/Delete/5
        [Authorize]
        public ActionResult Delete(int id = 0)
        {
            File file = _db.Files.Find(id);

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
            File file = _db.Files.Find(id);
            if (file.UserID != (Guid)Membership.GetUser().ProviderUserKey) { return HttpNotFound(); }
            file.IsDeactivated = true;
            _db.Entry(file).State = EntityState.Modified;
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