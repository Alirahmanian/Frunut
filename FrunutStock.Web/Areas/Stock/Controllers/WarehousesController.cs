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
using System.Collections;
using System.Diagnostics;
using FrunutStock.BL.General;


namespace FrunutStock.Web.Areas.Stock.Controllers
{
    public class WarehousesController : Controller
    {
        private FrunutStockEntities db = new FrunutStockEntities();
        private Relations relations = new Relations();
        // GET: Stock/Warehouses
        public ActionResult Index()
        {
            return View(db.Warehouses.ToList());
        }

        // GET: Stock/Warehouses/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Warehouse warehouse = db.Warehouses.Find(id);
            if (warehouse == null)
            {
                return HttpNotFound();
            }
            return View(warehouse);
        }

        // GET: Stock/Warehouses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Stock/Warehouses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Description,Address,Phone,AddedDate,ModifiedDate,UserName")] Warehouse warehouse)
        {
            if (ModelState.IsValid)
            {
                db.Warehouses.Add(warehouse);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(warehouse);
        }

        // GET: Stock/Warehouses/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Warehouse warehouse = db.Warehouses.Find(id);
            if (warehouse == null)
            {
                return HttpNotFound();
            }
            return View(warehouse);
        }

        // POST: Stock/Warehouses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Description,Address,Phone,AddedDate,ModifiedDate,UserName")] Warehouse warehouse)
        {
            if (ModelState.IsValid)
            {
                db.Entry(warehouse).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(warehouse);
        }

        // GET: Stock/Warehouses/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Warehouse warehouse = db.Warehouses.Find(id);
            if (warehouse == null)
            {
                return HttpNotFound();
            }
            return View(warehouse);
        }

        // POST: Stock/Warehouses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
           
            Warehouse warehouse = db.Warehouses.Find(id);
            
            var hasRelatedRows = relations.CheckRelatedRecords(db, "Warehouses", "WarehouseID", warehouse.ID);
           
            if (!hasRelatedRows)
            {
                db.Warehouses.Remove(warehouse);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                
                ViewBag.DeleteError = "Warehouse can not be deleted, because of relations with other entities.";
                ModelState.AddModelError(string.Empty, ViewBag.DeleteError);
                return View(warehouse);
            }

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
