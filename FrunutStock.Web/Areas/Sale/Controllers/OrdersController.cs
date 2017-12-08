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
    public class OrdersController : Controller
    {
        private FrunutStockEntities db = new FrunutStockEntities();

        // GET: Sale/Orders
        public ActionResult Index()
        {
            var orders = db.Orders.Include(o => o.Company).Include(o => o.Employee);
            return View(orders.ToList());
        }

        // GET: Sale/Orders/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Sale/Orders/Create
        public ActionResult Create(long? id)
        {
            
            ViewBag.CompanyID = new SelectList(db.Companies, "ID", "Name");
            ViewBag.EmpoyeeID = new SelectList(db.Employees, "ID", "FirstName");
            if (id != null)
            {
                var order = db.Orders.Include(o => o.Company).Include(o => o.Employee).Where(o => o.ID == id).First();
                order.OrderDetails = db.OrderDetails.Include(i => i.Item).Include(w => w.Warehouse).ToList();
                if (order != null)
                {
                    ViewBag.OrderID = order.ID;
                    ViewBag.HOrderID = order.ID;
                    return View(order);
                }
            }
            return View();
        }

        // POST: Sale/Orders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,CompanyID,EmpoyeeID,OrderDate,PaymentDate,LoadingDate,Coments,OrderdBy,Transport,Cash")] Order order)
        {
            if (ModelState.IsValid )
            {
                if (!String.IsNullOrEmpty(Request["HOrderID"]) )
                {
                    db.Entry(order).State = EntityState.Modified;
                    db.SaveChanges();
                    ViewBag.OrderID = order.ID;
                    ViewBag.HOrderID = order.ID;
                    ViewBag.CompanyID = new SelectList(db.Companies, "ID", "Name", order.CompanyID);
                    ViewBag.EmpoyeeID = new SelectList(db.Employees, "ID", "FirstName", order.EmpoyeeID);
                    return View(order);
                }
                
                order.AmountItem = 0;
                order.AmountReserve = 0;
                order.TotalPrice = 0;
                order.Coments = order.Coments != null ? order.Coments.Trim() : "";
                order.Coments += "_" + order.OrderDate.ToShortDateString();
                db.Orders.Add(order);
                db.SaveChanges();
                ViewBag.OrderID = order.ID;
                ViewBag.HOrderID = order.ID;
                ViewBag.CompanyID = new SelectList(db.Companies, "ID", "Name", order.CompanyID);
                ViewBag.EmpoyeeID = new SelectList(db.Employees, "ID", "FirstName", order.EmpoyeeID);
                return View(order);
               
            }
            
            ViewBag.CompanyID = new SelectList(db.Companies, "ID", "Name", order.CompanyID);
            ViewBag.EmpoyeeID = new SelectList(db.Employees, "ID", "FirstName", order.EmpoyeeID);
            return View(order);
        }

        // GET: Sale/Orders/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);

            order.OrderDetails = db.OrderDetails.Where(o => o.OrderID == order.ID).Include(i => i.Item).ToList();
            
            
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.CompanyID = new SelectList(db.Companies, "ID", "Name", order.CompanyID);
            ViewBag.EmpoyeeID = new SelectList(db.Employees, "ID", "FirstName", order.EmpoyeeID);
            return View(order);
        }

        // POST: Sale/Orders/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,CompanyID,EmpoyeeID,AmountItem,AmountReserve,TotalPrice,OrderDate,PaymentDate,PaidDate,LoadingDate,LoadedDate,AmountPaid,Coments,OrderdBy,Transport,PaymentWarning,ForcedPaid,OrderPaid,Cash,AddedDate,ModifiedDate,UserName")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CompanyID = new SelectList(db.Companies, "ID", "Name", order.CompanyID);
            ViewBag.EmpoyeeID = new SelectList(db.Employees, "ID", "FirstName", order.EmpoyeeID);
            return View(order);
        }

        // GET: Sale/Orders/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Sale/Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
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

        #region
        //JQuery Ajax calls go
        public JsonResult SaveOrderdetailsRow(OrderDetail orderDetail)
        {
            bool status = false;
            OrderDetail savedOrderRow = null; 
            try
            {
                ItemWarehouse itemWareHouse = db.ItemWarehouses.Find(orderDetail.WarehouseID);
                Order order = db.Orders.Find(orderDetail.OrderID);
                if (order != null && itemWareHouse != null)
                {
                    orderDetail.WarehouseID = itemWareHouse.WarehouseID;
                    db.OrderDetails.Add(orderDetail);
                    db.SaveChanges();
                    order.TotalPrice += orderDetail.Extended_Price;
                    db.Entry(order).State = EntityState.Modified;
                    db.SaveChanges();
                    savedOrderRow = order.OrderDetails.Where(od => od == orderDetail).FirstOrDefault();
                    if (savedOrderRow != null)
                    status = true;
                }
               
            }
            catch(Exception e)
            {

            }
            return new JsonResult
            {
                Data = new { status = status }
            };
            
        }
        public JsonResult GetItemGroups()
        {
            db.Configuration.ProxyCreationEnabled = false;
            var itemGroups = new List<ItemGroup>();
           itemGroups = db.ItemGroups.OrderBy(a => a.Name).ToList();
            db.Configuration.ProxyCreationEnabled = true;
            return new JsonResult { Data = itemGroups, JsonRequestBehavior = JsonRequestBehavior.AllowGet};
        }

        public JsonResult GetItemsByGroupID(Int64 groupId)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var items = new List<Item>();
            items = db.Items.Where(a => a.ItemGroupID == groupId).OrderBy(a => a.Name).ToList();
            db.Configuration.ProxyCreationEnabled = true;
            return new JsonResult { Data = items, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult GetItemWarehousesByItemID(Int64 itemId)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var itemWarehouse = (from iw in db.ItemWarehouses
                                 join i in db.Items on iw.ItemID equals i.ID
                                 join w in db.Warehouses on iw.WarehouseID equals w.ID
                                 orderby w.Name
                                 select new {
                                    ItemWarehouseID = iw.ID,
                                    ItemsonHand = w.Name + "|Box: " + iw.QtyBoxesOnhand.ToString() + "|Extra: " + iw.QtyKgOnhand.ToString() + "|Res. " +  iw.QtyBoxesReserved.ToString()
                                  }
                                );
            db.Configuration.ProxyCreationEnabled = true;
            return new JsonResult { Data = itemWarehouse, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        
        public PartialViewResult GetOrderDetails(int id = 0)
        {
            var orderDetails = db.OrderDetails.Where(d => d.OrderID == id).Include(i=> i.Item).Include(w=> w.Warehouse);
          
            return PartialView("_OrderDetails", orderDetails);
        }

        public JsonResult GetOrderDetailsForAjax(int id = 0)
        {
            var orderDetails = db.OrderDetails.Where(d => d.OrderID == id).Include(i => i.Item).Include(w => w.Warehouse);

            return new JsonResult { Data = orderDetails, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        #endregion

    }
}
