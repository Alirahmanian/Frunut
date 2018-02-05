using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrunutStock.Data;
using FrunutStock.Model.Models;

namespace FrunutStock.Service.Validations
{
   public class OrderDetailsValidation
   {
        public bool OrderDetailIsvalid(OrderDetail orderDetail)
        {
            if (orderDetail.OrderID <= 0) return false;
            if (orderDetail.ItemID <= 0) return false;
            if (orderDetail.WarehouseID <= 0) return false;
            if (orderDetail.QtyBoxes <= 0) return false;
            if (orderDetail.Price <= 0) return false;
            if (orderDetail.Extended_Price <= 0) return false;
            return true;
        }
        public bool HasOrderID(OrderDetail orderDetail)
        {
            return (orderDetail.OrderID <=0)? false : true;
        }

        public bool HasItemID(OrderDetail orderDetail)
        {
            return (orderDetail.ItemID <= 0) ? false : true;
        }
    }
}
