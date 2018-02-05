using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrunutStock.Data.Infrastructure;
using FrunutStock.Model.Models;

namespace FrunutStock.Data.Repositories
{
    public class ItemGroupRepository : RepositoryBase<ItemGroup>, IItemGroupRepository
    {
        public ItemGroupRepository(IDbFactory dbFactory) : base(dbFactory) { }
        public ItemGroup GetItemGroupById(Int64 itemGroupId)
        {
            var itemGroup = this.DbContext.ItemGroups.Where(g => g.ID == itemGroupId).FirstOrDefault();

            return itemGroup;
        }

        public ItemGroup GetItemGroupByName(string itemGroupName)
        {
            var itemGroup = this.DbContext.ItemGroups.Where(c => c.Name == itemGroupName).FirstOrDefault();

            return itemGroup;
        }
        
    }

    public interface IItemGroupRepository : IRepository<ItemGroup>
    {
        ItemGroup GetItemGroupById(Int64 itemGroupId);
        ItemGroup GetItemGroupByName(string itemGroupName);
    }
}

