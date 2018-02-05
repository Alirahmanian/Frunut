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
using FrunutStock.Service.Validations;
using FrunutStock.Service;

namespace FrunutStock.Web.Areas.Sale.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrderService orderService;
        private readonly ICompanyService companyService;
        private readonly IEmployeeService employeeService;
        
        // private FrunutStockEntities db = new FrunutStockEntities();
        //private OrderDetailsValidation orderDetailsValidation = new OrderDetailsValidation();


        public OrdersController(IOrderService orderService, ICompanyService companyService, IEmployeeService employeeService)
        {
            this.orderService = orderService;
            this.companyService = companyService;
            this.employeeService = employeeService;
        }
       
        // GET: Sale/Orders
        public ActionResult Index()
        {
            IEnumerable<Order> orders = orderService.GetOrders(null);
           // var orders = db.Orders.Include(o => o.Company).Include(o => o.Employee);
            return View(orders.ToList());
        }

        // GET: Sale/Orders/Details/5
        public ActionResult Details(Int64? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var orderId = id ?? 0;
            Order order = orderService.GetOrder(orderId);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Sale/Orders/Create
        public ActionResult Create(Int64? id)
        {
            
            ViewBag.CompanyID = new SelectList(companyService.GetCompanies(null), "ID", "Name");
            ViewBag.EmpoyeeID = new SelectList(employeeService.GetEmployees(null), "ID", "FullName");
            if (id != null)
            {
                var orderId = id ?? 0;
                var order = orderService.GetOrder(orderId);//.Include(o => o.Company).Include(o => o.Employee).Where(o => o.ID == id).First();
               // order.OrderDetails = db.OrderDetails.Include(i => i.Item).Include(w => w.Warehouse).ToList();
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
                    orderService.CreateOrder(order);//.Entry(order).State = EntityState.Modified;
                    orderService.SaveOrder();
                    ViewBag.OrderID = order.ID;
                    ViewBag.HOrderID = order.ID;
                    ViewBag.CompanyID = new SelectList(companyService.GetCompanies(null), "ID", "Name", order.CompanyID);
                    ViewBag.EmpoyeeID = new SelectList(employeeService.GetEmployees(null), "ID", "FUllName", order.EmpoyeeID);
                    return View(order);
                }
                
                order.AmountItem = 0;
                order.AmountReserve = 0;
                order.TotalPrice = 0;
                order.Coments = order.Coments != null ? order.Coments.Trim() : "";
                order.Coments += "_" + order.OrderDate.ToShortDateString();
                //order.PaidDate = null;
                // order.LoadedDate = null;
                orderService.CreateOrder(order);
                orderService.SaveOrder();
                ViewBag.OrderID = order.ID;
                ViewBag.HOrderID = order.ID;
                ViewBag.CompanyID = new SelectList(companyService.GetCompanies(null), "ID", "Name", order.CompanyID);
                ViewBag.EmpoyeeID = new SelectList(employeeService.GetEmployees(null), "ID", "FullName", order.EmpoyeeID);
                return View(order);
               
                //return RedirectToAction("Index");
            }
            
            ViewBag.CompanyID = new SelectList(companyService.GetCompanies(null), "ID", "Name", order.CompanyID);
            ViewBag.EmpoyeeID = new SelectList(employeeService.GetEmployees(null), "ID", "FullName", order.EmpoyeeID);
            return View(order);
        }

        // GET: Sale/Orders/Edit/5
        public ActionResult Edit(Int64? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var orderId = id ?? 0;
            Order order = orderService.GetOrder(orderId);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.CompanyID = new SelectList(companyService.GetCompanies(null), "ID", "Name", order.CompanyID);
            ViewBag.EmpoyeeID = new SelectList(employeeService.GetEmployees(null), "ID", "FullName", order.EmpoyeeID);
            return View(order);
        }

        // POST: Sale/Orders/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,CompanyID,EmpoyeeID,AmountItem,AmountReserve,TotalPrice,OrderDate,PaymentDate,PaidDate,LoadingDate,LoadedDate,AmountPaid,Coments,OrderdBy,Transport,PaymentWarning,ForcedPaid,OrderPaid,Cash,AddedDate,ModifiedDate,UserName")] Order order)
        {
            if (ModelState.IsValid)
            {
                orderService.UpdateOrder(order);
                orderService.SaveOrder();
                return RedirectToAction("Index");
            }
            ViewBag.CompanyID = new SelectList(companyService.GetCompanies(null), "ID", "Name", order.CompanyID);
            ViewBag.EmpoyeeID = new SelectList(employeeService.GetEmployees(null), "ID", "FirstName", order.EmpoyeeID);
            return View(order);
        }

        // GET: Sale/Orders/Delete/5
        public ActionResult Delete(Int64? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var orderId = id ?? 0;
            Order order = orderService.GetOrder(orderId);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Sale/Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Int64 id)
        {
            Order order = orderService.GetOrder(id);
            orderService.DeleteOrder(order);
            orderService.SaveOrder();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
                
            }
            base.Dispose(disposing);
        }

        

    }
}
