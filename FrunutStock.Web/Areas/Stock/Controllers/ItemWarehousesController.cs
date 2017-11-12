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
    public class ItemWarehousesController : Controller
    {
        private FrunutStockEntities db = new FrunutStockEntities();

        // GET: Stock/ItemWarehouses
        public ActionResult Index()
        {
            var itemWarehouses = db.ItemWarehouses.Include(i => i.Item).Include(i => i.Warehouse);
            return View(itemWarehouses.ToList());
        }

        // GET: Stock/ItemWarehouses/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemWarehouse itemWarehouse = db.ItemWarehouses.Find(id);
            if (itemWarehouse == null)
            {
                return HttpNotFound();
            }
            return View(itemWarehouse);
        }
/*
        // GET: Stock/ItemWarehouses/Create
        public ActionResult Create()
        {
            ViewBag.ItemID = new SelectList(db.Items, "ID", "Name");
            ViewBag.WarehouseID = new SelectList(db.Warehouses, "ID", "Name");
            return View();
        }

        // POST: Stock/ItemWarehouses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ItemID,WarehouseID,QtyBoxesIn,QtyKgIn,QtyTotalWeightIn,QtyBoxesOnhand,QtyKgOnhand,QtyTotalWeightOnhand,QtyBoxesReserved,QtyTotalWeightReserved,AddedDate,ModifiedDate,UserName")] ItemWarehouse itemWarehouse)
        {
            if (ModelState.IsValid)
            {
                db.ItemWarehouses.Add(itemWarehouse);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ItemID = new SelectList(db.Items, "ID", "Name", itemWarehouse.ItemID);
            ViewBag.WarehouseID = new SelectList(db.Warehouses, "ID", "Name", itemWarehouse.WarehouseID);
            return View(itemWarehouse);
        }

        // GET: Stock/ItemWarehouses/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemWarehouse itemWarehouse = db.ItemWarehouses.Find(id);
            if (itemWarehouse == null)
            {
                return HttpNotFound();
            }
            ViewBag.ItemID = new SelectList(db.Items, "ID", "Name", itemWarehouse.ItemID);
            ViewBag.WarehouseID = new SelectList(db.Warehouses, "ID", "Name", itemWarehouse.WarehouseID);
            return View(itemWarehouse);
        }

        // POST: Stock/ItemWarehouses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ItemID,WarehouseID,QtyBoxesIn,QtyKgIn,QtyTotalWeightIn,QtyBoxesOnhand,QtyKgOnhand,QtyTotalWeightOnhand,QtyBoxesReserved,QtyTotalWeightReserved,AddedDate,ModifiedDate,UserName")] ItemWarehouse itemWarehouse)
        {
            if (ModelState.IsValid)
            {
                db.Entry(itemWarehouse).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ItemID = new SelectList(db.Items, "ID", "Name", itemWarehouse.ItemID);
            ViewBag.WarehouseID = new SelectList(db.Warehouses, "ID", "Name", itemWarehouse.WarehouseID);
            return View(itemWarehouse);
        }

        // GET: Stock/ItemWarehouses/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemWarehouse itemWarehouse = db.ItemWarehouses.Find(id);
            if (itemWarehouse == null)
            {
                return HttpNotFound();
            }
            return View(itemWarehouse);
        }

        // POST: Stock/ItemWarehouses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            ItemWarehouse itemWarehouse = db.ItemWarehouses.Find(id);
            db.ItemWarehouses.Remove(itemWarehouse);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
*/
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
