using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrunutStock.Data.Infrastructure;
using FrunutStock.Model.Models;
using System.Data.Entity;

namespace FrunutStock.Data.Repositories
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(IDbFactory dbFactory) : base(dbFactory){ }
        public Order GetOrderById(Int64 orderId)
        {
            var order = this.DbContext.Orders.Where(o => o.ID == orderId).FirstOrDefault();
            return order;
        }

        public IEnumerable<Order> GetOrderWithIncludes()
        {
            var orders = this.DbContext.Orders.Include(o => o.Company).Include(o => o.Employee);
            return orders;
        }
    }

    public interface IOrderRepository : IRepository<Order>
    {
        Order GetOrderById(Int64 orderId);
        IEnumerable<Order> GetOrderWithIncludes();
    }
}
