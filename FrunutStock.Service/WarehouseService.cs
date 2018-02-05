using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrunutStock.Data.Infrastructure;
using FrunutStock.Data.Repositories;
using FrunutStock.Model.Models;

namespace FrunutStock.Service
{
    // operations you want to expose
    public interface IWarehouseService
    {
        IEnumerable<Warehouse> GetWarehouses(Int64? id);
        Warehouse GetWarehouse(Int64 id);
        Warehouse GetWarehouseByName(string warehouseName);
        void CreateWarehouse(Warehouse warehouse);
        void SaveWarehouse();
    }

    public class WarehouseService : IWarehouseService
    {
        private readonly IWarehouseRepository warehouseRepository;
        private readonly IUnitOfWork unitOfWork;

        public WarehouseService(IWarehouseRepository warehouseRepository, IUnitOfWork unitOfWork)
        {
            this.warehouseRepository = warehouseRepository;
            this.unitOfWork = unitOfWork;
        }

        #region IWarehouseService Members

        public IEnumerable<Warehouse> GetWarehouses(Int64? id)
        {
            if (id == null || id <= 0)
                return warehouseRepository.GetAll();
            else
                return warehouseRepository.GetAll().Where(o => o.ID == id);
        }

        public Warehouse GetWarehouse(Int64 id)
        {
            var warehouse = warehouseRepository.GetById(id);
            return warehouse;
        }

        public Warehouse GetWarehouseByName(string warehouseName)
        {
            return warehouseRepository.Get(c => c.Name == warehouseName);
        }

        public void CreateWarehouse(Warehouse warehouse)
        {
            warehouseRepository.Add(warehouse);
        }

        public void SaveWarehouse()
        {
            unitOfWork.Commit();
        }

        #endregion
    }
}

