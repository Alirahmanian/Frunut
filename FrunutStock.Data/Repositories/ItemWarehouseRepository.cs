using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrunutStock.Data.Infrastructure;
using FrunutStock.Model.Models;

namespace FrunutStock.Data.Repositories
{
    public class ItemWarehouseRepository : RepositoryBase<ItemWarehouse>, IItemWarehouseRepository
    {
        public ItemWarehouseRepository(IDbFactory dbFactory) : base(dbFactory) { }
        public ItemWarehouse GetItemWarehouseById(Int64 itemWarehouseId)
        {
            var itemWarehouse = this.DbContext.ItemWarehouses.Where(g => g.ID == itemWarehouseId).FirstOrDefault();
            return itemWarehouse;
        }

        public bool UpdateItemWarehouseByOrderDetail(OrderDetail orderDetail)
        {

            return false;
        }

        public bool UpdateItemWarehouseByOrderDetail(OrderDetail orderDetail, int factor)
        {
            return false;
        }
        public bool UpdateItemWarehouseByNewOrderDetail(OrderDetail orderDetail)
        {
            return false;
        }
      

    }

    public interface IItemWarehouseRepository : IRepository<ItemWarehouse>
    {
        ItemWarehouse GetItemWarehouseById(Int64 itemWarehouseId);
        bool UpdateItemWarehouseByNewOrderDetail(OrderDetail orderDetail);
    }
}

