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
    public class ItemsController : Controller
    {
        private FrunutStockEntities db = new FrunutStockEntities();

        // GET: Stock/Items
        public ActionResult Index()
        {
            var items = db.Items.Include(i => i.Country).Include(i => i.ItemGroup);
            return View(items.ToList());
        }

        // GET: Stock/Items/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // GET: Stock/Items/Create
        public ActionResult Create()
        {
            ViewBag.CountryID = new SelectList(db.Countries, "ID", "Name");
            ViewBag.ItemGroupID = new SelectList(db.ItemGroups, "ID", "Name");
            return View();
        }

        // POST: Stock/Items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Description,MinimumPrice,BoxWeight,TotalBoxesOnHand,TotalWeightOnHand,TotalBoxesReserved,TotalWeightReserved,ItemGroupID,CountryID,AddedDate,ModifiedDate,UserName")] Item item)
        {
            if (ModelState.IsValid)
            {
                db.Items.Add(item);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CountryID = new SelectList(db.Countries, "ID", "Name", item.CountryID);
            ViewBag.ItemGroupID = new SelectList(db.ItemGroups, "ID", "Name", item.ItemGroupID);
            return View(item);
        }

        // GET: Stock/Items/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            ViewBag.CountryID = new SelectList(db.Countries, "ID", "Name", item.CountryID);
            ViewBag.ItemGroupID = new SelectList(db.ItemGroups, "ID", "Name", item.ItemGroupID);
            return View(item);
        }

        // POST: Stock/Items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Description,MinimumPrice,BoxWeight,TotalBoxesOnHand,TotalWeightOnHand,TotalBoxesReserved,TotalWeightReserved,ItemGroupID,CountryID,AddedDate,ModifiedDate,UserName")] Item item)
        {
            if (ModelState.IsValid)
            {
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CountryID = new SelectList(db.Countries, "ID", "Name", item.CountryID);
            ViewBag.ItemGroupID = new SelectList(db.ItemGroups, "ID", "Name", item.ItemGroupID);
            return View(item);
        }

        // GET: Stock/Items/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: Stock/Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Item item = db.Items.Find(id);
            db.Items.Remove(item);
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
