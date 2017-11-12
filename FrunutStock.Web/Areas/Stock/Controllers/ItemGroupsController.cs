using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FrunutStock.Data;
using FrunutStock.Model.Models;

namespace FrunutStock.Web.Areas.Stock.Controllers
{
    public class ItemGroupsController : Controller
    {
        private FrunutStockEntities db = new FrunutStockEntities();

        // GET: Stock/ItemGroups
        public ActionResult Index()
        {
            return View(db.ItemGroups.ToList());
        }

        // GET: Stock/ItemGroups/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemGroup itemGroup = db.ItemGroups.Find(id);
            if (itemGroup == null)
            {
                return HttpNotFound();
            }
            return View(itemGroup);
        }

        // GET: Stock/ItemGroups/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Stock/ItemGroups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,AddedDate,ModifiedDate,UserName")] ItemGroup itemGroup)
        {
            if (ModelState.IsValid)
            {
                db.ItemGroups.Add(itemGroup);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(itemGroup);
        }

        // GET: Stock/ItemGroups/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemGroup itemGroup = db.ItemGroups.Find(id);
            if (itemGroup == null)
            {
                return HttpNotFound();
            }
            return View(itemGroup);
        }

        // POST: Stock/ItemGroups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,AddedDate,ModifiedDate,UserName")] ItemGroup itemGroup)
        {
            if (ModelState.IsValid)
            {
                db.Entry(itemGroup).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(itemGroup);
        }

        // GET: Stock/ItemGroups/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemGroup itemGroup = db.ItemGroups.Find(id);
            if (itemGroup == null)
            {
                return HttpNotFound();
            }
            return View(itemGroup);
        }

        // POST: Stock/ItemGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            ItemGroup itemGroup = db.ItemGroups.Find(id);
            db.ItemGroups.Remove(itemGroup);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
