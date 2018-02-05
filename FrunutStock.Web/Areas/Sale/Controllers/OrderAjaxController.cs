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
    public class OrderAjaxController : Controller
    {
        private readonly IOrderService orderService;
        private readonly IOrderDetailService orderDetailService;
        private readonly IItemGroupService itemgroupService;
        private readonly IItemService itemService;
        private readonly IWarehouseService warehouseService;
        private readonly IItemWarehouseService itemWarehouseService;
       

        public OrderAjaxController(IOrderService orderService, IOrderDetailService orderDetailService
           , IItemGroupService itemgroupService, IItemService itemService, IWarehouseService warehouseService, IItemWarehouseService itemWarehouseService)
        {
            this.orderService = orderService;
            this.orderDetailService = orderDetailService;
            this.itemgroupService = itemgroupService;
            this.itemService = itemService;
            this.warehouseService = warehouseService;
            this.itemWarehouseService = itemWarehouseService;
        }

        // GET: Sale/OrderAjax
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetItemGroups()
        {
            //db.Configuration.ProxyCreationEnabled = false;
            List<ItemGroup> itemGroups = new List<ItemGroup>();
            itemGroups = itemgroupService.GetItemGroups(null).OrderBy(a => a.Name).ToList();
            // db.Configuration.ProxyCreationEnabled = true;
            return new JsonResult { Data = itemGroups, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        //JQuery Ajax calls go
        public JsonResult SaveOrderdetailsRow(OrderDetail orderDetail)
        {
            bool status = false;
            OrderDetail savedOrderRow = null;
            try
            {
                ItemWarehouse itemWareHouse = itemWarehouseService.GetItemWarehouse(orderDetail.WarehouseID);
                Order order = orderService.GetOrder(orderDetail.OrderID);
                if (order != null)
                {
                    orderDetail.WarehouseID = itemWareHouse.WarehouseID;
                    orderDetailService.CreateOrderDetail(orderDetail);
                    order.TotalPrice += orderDetail.Extended_Price;
                    orderService.UpdateOrder(order);
                    orderDetailService.SaveOrderDetail();
                    orderService.SaveOrder();
                    savedOrderRow = orderDetailService.GetSavedOrderDetail(orderDetail);
                    if (savedOrderRow != null)
                     status = true;
                }
            }
            catch (Exception e)
            {

            }
            return new JsonResult
            {
                Data = new { status = status }
            };

        }



        public JsonResult GetItemsByGroupID(Int64 groupId)
        {
            //db.Configuration.ProxyCreationEnabled = false;
            List<Item> items = new List<Item>();
            items = itemService.GetItemsByItemGroupId(groupId).ToList();
            // db.Configuration.ProxyCreationEnabled = true;
            return new JsonResult { Data = items, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult GetItemWarehousesByItemID(Int64 itemId)
        {
            // db.Configuration.ProxyCreationEnabled = false;
            var itemWarehouse = itemService.GetItemWarehousesByItemID(itemId);
            //db.Configuration.ProxyCreationEnabled = true;
            return new JsonResult { Data = itemWarehouse, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        //
        public PartialViewResult GetOrderDetails(Int64 id = 0)
        {
            var orderDetails = orderDetailService.GetOrderDetailsByOrder(id);

            return PartialView("_OrderDetails", orderDetails);
        }

        public JsonResult GetOrderDetailsForAjax(Int64 id = 0)
        {
            var orderDetails = orderDetailService.GetOrderDetailsByOrder(id);
            return new JsonResult { Data = orderDetails, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

    }
}