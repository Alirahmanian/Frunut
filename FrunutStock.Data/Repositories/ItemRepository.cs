using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrunutStock.Data.Infrastructure;
using FrunutStock.Model.Models;

namespace FrunutStock.Data.Repositories
{
    public class ItemRepository : RepositoryBase<Item>, IItemRepository
    {
        public ItemRepository(IDbFactory dbFactory) : base(dbFactory) { }
        public Item GeItemById(Int64 itemId)
        {
            var item = this.DbContext.Items.Where(i => i.ID == itemId).FirstOrDefault();

            return item;
        }

        public Item GetItemByName(string itemName)
        {
            var item = this.DbContext.Items.Where(c => c.Name == itemName).FirstOrDefault();

            return item;
        }

        public IEnumerable<Item> GetItemsByItemGroupId(Int64 itemGroupId)
        {
            var items = this.DbContext.Items.Where(i => i.ItemGroupID == itemGroupId).OrderBy(i => i.Name);
            return items;
        }

        public IQueryable GetItemWarehousesByItemID(Int64 itemId)
        {
            var itemWarehouse = (from iw in this.DbContext.ItemWarehouses
                                 join i in this.DbContext.Items on iw.ItemID equals i.ID
                                 join w in this.DbContext.Warehouses on iw.WarehouseID equals w.ID
                                 orderby w.Name
                                 select new
                                 {
                                     ItemWarehouseID = iw.ID,
                                     ItemsonHand = w.Name + "|Box: " + iw.QtyBoxesOnhand.ToString() + "|Extra: " + iw.QtyKgOnhand.ToString() + "|Res. " + iw.QtyBoxesReserved.ToString()
                                 }
                                );
            return itemWarehouse;
        }

    }

    public interface IItemRepository : IRepository<Item>
    {
        Item GeItemById(Int64 itemId);
        Item GetItemByName(string itemName);
        IEnumerable<Item> GetItemsByItemGroupId(Int64 itemGroupId);
        IQueryable GetItemWarehousesByItemID(Int64 itemId);

    }
}


