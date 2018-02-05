using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using FrunutStock.Data.Infrastructure;
using FrunutStock.Model.Models;

namespace FrunutStock.Data.Repositories
{
    public class OrderDetailRepository : RepositoryBase<OrderDetail>, IOrderDetailRepository
    {
        public OrderDetailRepository(IDbFactory dbFactory) : base(dbFactory) { }
        public OrderDetail GetOrderDetailById(Int64 orderDetailId)
        {
            var orderDetail = this.DbContext.OrderDetails.Where(d => d.ID == orderDetailId).FirstOrDefault();
            return orderDetail;
        }

        public IEnumerable<OrderDetail> GetOrderDetailsByOrderId(Int64 orderId)
        {
           var orderDetails = this.DbContext.OrderDetails.Where(d => d.OrderID == orderId).Include(i => i.Item).Include(w => w.Warehouse).OrderBy(d => d.ID).ToList();
           return orderDetails;
        }

        public OrderDetail GetSavedOrderDetail(OrderDetail orderDetail)
        {
            var savedOrderDetail = new OrderDetail();
            var order = this.DbContext.Orders.Where(o => o.ID == orderDetail.OrderID).FirstOrDefault();
            if (order != null)
            {
                savedOrderDetail = order.OrderDetails.Where(d => d == orderDetail).FirstOrDefault();
            }
            return savedOrderDetail;

        }

    }

    public interface IOrderDetailRepository : IRepository<OrderDetail>
    {
        OrderDetail GetOrderDetailById(Int64 orderDetailId);
        IEnumerable<OrderDetail> GetOrderDetailsByOrderId(Int64 orderId);
        OrderDetail GetSavedOrderDetail(OrderDetail orderDetail);

    }
}
