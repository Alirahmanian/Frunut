using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrunutStock.Data.Infrastructure;
using FrunutStock.Model.Models;

namespace FrunutStock.Data.Repositories
{
    public class WarehouseRepository : RepositoryBase<Warehouse>, IWarehouseRepository
    {
        public WarehouseRepository(IDbFactory dbFactory) : base(dbFactory) { }
        public Warehouse GetWarehouseById(Int64 warehouseId)
        {
            var warehouse = this.DbContext.Warehouses.Where(g => g.ID == warehouseId).FirstOrDefault();

            return warehouse;
        }

        public Warehouse GetWarehouseByName(string warehouseName)
        {
            var warehouse = this.DbContext.Warehouses.Where(c => c.Name == warehouseName).FirstOrDefault();

            return warehouse;
        }
      
    }

    public interface IWarehouseRepository : IRepository<Warehouse>
    {
        Warehouse GetWarehouseById(Int64 warehouseId);
        Warehouse GetWarehouseByName(string warehouseName);
    }
}


