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
    public interface IItemGroupService
    {
        IEnumerable<ItemGroup> GetItemGroups(Int64? id);
        ItemGroup GetItemGroup(Int64 id);
        ItemGroup GetItemGroupByName(string itemGroupName);
        void CreateItemGroup(ItemGroup itemGroup);
        void SaveItemGroup();
    }

    public class ItemGroupService : IItemGroupService
    {
        private readonly IItemGroupRepository itemGroupRepository;
        private readonly IUnitOfWork unitOfWork;

        public ItemGroupService(IItemGroupRepository itemGroupRepository, IUnitOfWork unitOfWork)
        {
            this.itemGroupRepository = itemGroupRepository;
            this.unitOfWork = unitOfWork;
        }

        #region IWarehouseService Members

        public IEnumerable<ItemGroup> GetItemGroups(Int64? id)
        {
            if (id == null || id <= 0)
                return itemGroupRepository.GetAll();
            else
                return itemGroupRepository.GetAll().Where(o => o.ID == id);
        }

        public ItemGroup GetItemGroup(Int64 id)
        {
            var itemGroup = itemGroupRepository.GetById(id);
            return itemGroup;
        }

        public ItemGroup GetItemGroupByName(string itemGroupName)
        {
            return itemGroupRepository.Get(c => c.Name == itemGroupName);
        }

        public void CreateItemGroup(ItemGroup itemGroup)
        {
            itemGroupRepository.Add(itemGroup);
        }

        public void SaveItemGroup()
        {
            unitOfWork.Commit();
        }

        #endregion
    }
}

