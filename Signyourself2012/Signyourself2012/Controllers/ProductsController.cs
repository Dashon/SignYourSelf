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
    public class ProductsController : Controller
    {
        private SignYourselfEntities db = new SignYourselfEntities();

        //
        // GET: /Products/
        [Authorize]
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.ProductType).Include(p => p.Seller);
            return View(products.ToList());
        }

        //
        // GET: /Products/Details/5

        public ActionResult Details(int id = 0)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        //
        // GET: /Products/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.ProductTypeID = new SelectList(db.ProductTypes, "ProductTypeID", "Name");
            ViewBag.UserID = new SelectList(db.Users, "UserId", "UserName");
            return View();
        }

        //
        // POST: /Products/Create

        [HttpPost]
        [Authorize]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductTypeID = new SelectList(db.ProductTypes, "ProductTypeID", "Name", product.ProductTypeID);
            ViewBag.UserID = new SelectList(db.Users, "UserId", "UserName", product.UserID);
            return View(product);
        }

        //
        // GET: /Products/Edit/5
        [Authorize]
        public ActionResult Edit(int id = 0)
        {
            Product product = db.Products.Find(id);
            if (product.UserID != (Guid)Membership.GetUser().ProviderUserKey) { return HttpNotFound(); }
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductTypeID = new SelectList(db.ProductTypes, "ProductTypeID", "Name", product.ProductTypeID);
            ViewBag.UserID = new SelectList(db.Users, "UserId", "UserName", product.UserID);
            return View(product);
        }

        //
        // POST: /Products/Edit/5

        [HttpPost]
        [Authorize]
        public ActionResult Edit(Product product)
        {
            if (product.UserID != (Guid)Membership.GetUser().ProviderUserKey) { return HttpNotFound(); }
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductTypeID = new SelectList(db.ProductTypes, "ProductTypeID", "Name", product.ProductTypeID);
            ViewBag.UserID = new SelectList(db.Users, "UserId", "UserName", product.UserID);
            return View(product);
        }

        //
        // GET: /Products/Delete/5
        [Authorize]
        public ActionResult Delete(int id = 0)
        {

            Product product = db.Products.Find(id);
            if (product.UserID != (Guid)Membership.GetUser().ProviderUserKey) { return HttpNotFound(); }
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        //
        // POST: /Products/Delete/5

        [HttpPost, ActionName("Delete")]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            if (product.UserID != (Guid)Membership.GetUser().ProviderUserKey) { return HttpNotFound(); }
            db.Products.Remove(product);
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