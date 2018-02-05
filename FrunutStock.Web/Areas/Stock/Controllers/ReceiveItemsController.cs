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
using FrunutStock.BL.Stock;

namespace FrunutStock.Web.Areas.Stock.Controllers
{
    public class ReceiveItemsController : Controller
    {
        private FrunutStockEntities db = new FrunutStockEntities();
        private ItemInOut itemInout = new ItemInOut();

        // GET: Stock/AddItems
        public ActionResult Index()
        {
            var receiveItems = db.ReceiveItems.Include(a => a.Item).Include(a => a.Warehouse).Include(a => a.Company);
            return View(receiveItems.ToList());
        }

        // GET: Stock/AddItems/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReceiveItem addItem = db.ReceiveItems.Find(id);
            if (addItem == null)
            {
                return HttpNotFound();
            }
            return View(addItem);
        }

        // GET: Stock/AddItems/Create
        public ActionResult Create()
        {
            ViewBag.ItemID = new SelectList(db.Items, "ID", "Name");
            ViewBag.WarehouseID = new SelectList(db.Warehouses, "ID", "Name");
            return View();
        }

        // POST: Stock/AddItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Date,ItemID,WarehouseID,CompanyID,Description,QtyBoxes,QtyKg,AddedDate,UserName")] ReceiveItem addItem)
        {
            if (ModelState.IsValid)
            {
               // db.AddItems.Add(addItem);
                bool result = itemInout.CreateReceiveItem(db, addItem);
                //db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ItemID = new SelectList(db.Items, "ID", "Name", addItem.ItemID);
            ViewBag.WarehouseID = new SelectList(db.Warehouses, "ID", "Name", addItem.WarehouseID);
            return View(addItem);
        }

        // GET: Stock/AddItems/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReceiveItem addItem = db.ReceiveItems.Find(id);
            if (addItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.ItemID = new SelectList(db.Items, "ID", "Name", addItem.ItemID);
            ViewBag.WarehouseID = new SelectList(db.Warehouses, "ID", "Name", addItem.WarehouseID);
            return View(addItem);
        }

        // POST: Stock/AddItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Date,ItemID,WarehouseID,Description,QtyBoxes,QtyKg,AddedDate,ModifiedDate,UserName")] ReceiveItem addItem)
        {
            if (ModelState.IsValid)
            {
                bool result = itemInout.EditReceiveItem(db, addItem);
                //db.Entry(addItem).State = EntityState.Modified;
                //db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ItemID = new SelectList(db.Items, "ID", "Name", addItem.ItemID);
            ViewBag.WarehouseID = new SelectList(db.Warehouses, "ID", "Name", addItem.WarehouseID);
            return View(addItem);
        }

        // GET: Stock/AddItems/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReceiveItem addItem = db.ReceiveItems.Find(id);
            if (addItem == null)
            {
                return HttpNotFound();
            }
            return View(addItem);
        }

        // POST: Stock/AddItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            ReceiveItem addItem = db.ReceiveItems.Find(id);
            bool result = itemInout.DeleteReceiveItem(db, addItem);
            //db.AddItems.Remove(addItem);
            //db.SaveChanges();
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
