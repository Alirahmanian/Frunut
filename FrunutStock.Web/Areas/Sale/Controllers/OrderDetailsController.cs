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

namespace FrunutStock.Web.Areas.Sale.Controllers
{
    public class OrderDetailsController : Controller
    {
        private FrunutStockEntities db = new FrunutStockEntities();

        // GET: Sale/OrderDetails
        public ActionResult Index()
        {
            var orderDetails = db.OrderDetails.Include(o => o.Item).Include(o => o.Order).Include(o => o.Warehouse);
            return View(orderDetails.ToList());
        }

        // GET: Sale/OrderDetails/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderDetail = db.OrderDetails.Find(id);
            if (orderDetail == null)
            {
                return HttpNotFound();
            }
            return View(orderDetail);
        }

        // GET: Sale/OrderDetails/Create
        public ActionResult Create()
        {
            ViewBag.ItemID = new SelectList(db.Items, "ID", "Name");
            ViewBag.OrderID = new SelectList(db.Orders, "ID", "Coments");
            ViewBag.WarehouseID = new SelectList(db.Warehouses, "ID", "Name");
            return View();
        }

        // POST: Sale/OrderDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,OrderID,ItemID,WarehouseID,Price,Extended_Price,QtyBoxes,QtyReservBoxes,QtyKg,AddedDate,ModifiedDate,UserName")] OrderDetail orderDetail)
        {
            if (ModelState.IsValid)
            {
                db.OrderDetails.Add(orderDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ItemID = new SelectList(db.Items, "ID", "Name", orderDetail.ItemID);
            ViewBag.OrderID = new SelectList(db.Orders, "ID", "Coments", orderDetail.OrderID);
            ViewBag.WarehouseID = new SelectList(db.Warehouses, "ID", "Name", orderDetail.WarehouseID);
            return View(orderDetail);
        }

        // GET: Sale/OrderDetails/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderDetail = db.OrderDetails.Find(id);
            if (orderDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.ItemID = new SelectList(db.Items, "ID", "Name", orderDetail.ItemID);
            ViewBag.OrderID = new SelectList(db.Orders, "ID", "Coments", orderDetail.OrderID);
            ViewBag.WarehouseID = new SelectList(db.Warehouses, "ID", "Name", orderDetail.WarehouseID);
            return View(orderDetail);
        }

        // POST: Sale/OrderDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,OrderID,ItemID,WarehouseID,Price,Extended_Price,QtyBoxes,QtyReservBoxes,QtyKg,AddedDate,ModifiedDate,UserName")] OrderDetail orderDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ItemID = new SelectList(db.Items, "ID", "Name", orderDetail.ItemID);
            ViewBag.OrderID = new SelectList(db.Orders, "ID", "Coments", orderDetail.OrderID);
            ViewBag.WarehouseID = new SelectList(db.Warehouses, "ID", "Name", orderDetail.WarehouseID);
            return View(orderDetail);
        }

        // GET: Sale/OrderDetails/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderDetail = db.OrderDetails.Find(id);
            if (orderDetail == null)
            {
                return HttpNotFound();
            }
            return View(orderDetail);
        }

        // POST: Sale/OrderDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            OrderDetail orderDetail = db.OrderDetails.Find(id);
            db.OrderDetails.Remove(orderDetail);
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
