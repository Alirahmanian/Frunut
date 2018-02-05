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
    public interface IItemWarehouseService
    {
        IEnumerable<ItemWarehouse> GetItemWarehouses(Int64? id);
        ItemWarehouse GetItemWarehouse(Int64 id);
        void CreateItemWarehouse(ItemWarehouse itemWarehouse);
        void SaveItemWarehouse();
    }

    public class ItemWarehouseService : IItemWarehouseService
    {
        private readonly IItemWarehouseRepository itemWarehouseRepository;
        private readonly IUnitOfWork unitOfWork;

        public ItemWarehouseService(IItemWarehouseRepository itemWarehouseRepository, IUnitOfWork unitOfWork)
        {
            this.itemWarehouseRepository = itemWarehouseRepository;
            this.unitOfWork = unitOfWork;
        }

        #region IWarehouseService Members

        public IEnumerable<ItemWarehouse> GetItemWarehouses(Int64? id)
        {
            if (id == null || id <= 0)
                return itemWarehouseRepository.GetAll();
            else
                return itemWarehouseRepository.GetAll().Where(o => o.ID == id);
        }

        public ItemWarehouse GetItemWarehouse(Int64 id)
        {
            var itemWarehouse = itemWarehouseRepository.GetById(id);
            return itemWarehouse;
        }

        public void CreateItemWarehouse(ItemWarehouse itemWarehouse)
        {
            itemWarehouseRepository.Add(itemWarehouse);
        }

        public void SaveItemWarehouse()
        {
            unitOfWork.Commit();
        }

        #endregion
    }
}


