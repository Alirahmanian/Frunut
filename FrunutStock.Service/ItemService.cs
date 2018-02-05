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
    public interface IItemService
    {
        IEnumerable<Item> GetItems(Int64? id);
        Item GetItem(Int64 id);
        Item GetItemByName(string itemName);
        IEnumerable<Item> GetItemsByItemGroupId(Int64 itemGroupId);
        IQueryable GetItemWarehousesByItemID(Int64 itemId);
        void CreateItem(Item item);
        void SaveItem();
    }

    public class ItemService : IItemService
    {
        private readonly IItemRepository itemRepository;
        private readonly IUnitOfWork unitOfWork;

        public ItemService(IItemRepository itemRepository, IUnitOfWork unitOfWork)
        {
            this.itemRepository = itemRepository;
            this.unitOfWork = unitOfWork;
        }

        #region IItemService Members

        public IEnumerable<Item> GetItems(Int64? id)
        {
            if (id == null || id <= 0)
                return itemRepository.GetAll();
            else
                return itemRepository.GetAll().Where(o => o.ID == id);
        }

        public Item GetItem(Int64 id)
        {
            var item = itemRepository.GetById(id);
            return item;
        }

        public Item GetItemByName(string itemName)
        {
            return itemRepository.Get(i => i.Name == itemName);
        }

        public IEnumerable<Item> GetItemsByItemGroupId(Int64 itemGroupId)
        {
            return itemRepository.GetItemsByItemGroupId(itemGroupId);
        }

        public IQueryable GetItemWarehousesByItemID(Int64 itemId)
        {
            return itemRepository.GetItemWarehousesByItemID(itemId);
        }

        public void CreateItem(Item item)
        {
            itemRepository.Add(item);
        }

        public void SaveItem()
        {
            unitOfWork.Commit();
        }

        #endregion
    }
}

