using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using FrunutStock.Model.Models;
using FrunutStock.Data;

namespace FrunutStock.BL.Stock
{
    public class ItemInOut
    {
        
        public ItemInOut()
        {
           
        }
        #region AddItem Crud
        public bool CreateAddItem(FrunutStockEntities db, AddItem addItem)
        {
            bool result = false;
            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                var itemWarehouse = db.ItemWarehouses.Where(i => i.ItemID == addItem.ItemID && i.WarehouseID == addItem.WarehouseID).FirstOrDefault();
                var item = db.Items.Where(i => i.ID == addItem.ItemID).FirstOrDefault();
                try
                {
                    if (itemWarehouse == null)
                    {
                       ItemWarehouse iw = AddItemWarehouse(addItem, item);
                        db.ItemWarehouses.Add(iw);
                    }
                    else
                    {
                        UpdateItemWarehouse(itemWarehouse, addItem, item, 1);
                        db.Entry(itemWarehouse).State = EntityState.Modified;
                    }
                   
                    db.AddItems.Add(addItem);
                    
                    db.SaveChanges();
                    dbContextTransaction.Commit();
                    result = true;
                }
                catch (Exception err)
                {
                   // UpdateItemWarehouse(itemWarehouse, addItem, -1);
                    dbContextTransaction.Rollback();
                    result = false;
                }
            }
            return result;
        }

        public bool EditAddItem(FrunutStockEntities db, AddItem addItem)
        {
            bool result = false;
            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                var oldAddItem = db.AddItems.Find(addItem.ID);
                
                try
                {
                    if (oldAddItem == null)
                    {
                        //dbContextTransaction.Rollback();
                        //result = false;
                        throw new System.InvalidOperationException("Could't find old values.");
                    }

                    var oldItemWarehouse = db.ItemWarehouses.Where(i => i.ItemID == oldAddItem.ItemID && i.WarehouseID == oldAddItem.WarehouseID).FirstOrDefault();
                    var oldItem = db.Items.Where(i => i.ID == oldAddItem.ItemID).FirstOrDefault();
                    if (oldItemWarehouse == null)
                    {
                        throw new System.InvalidOperationException("Could't find old values.");
                    }

                        
                    UpdateItemWarehouse(oldItemWarehouse, oldAddItem, oldItem, -1);
                    db.Entry(oldItemWarehouse).State = EntityState.Modified;
                    db.SaveChanges();

                    var itemWarehouse = db.ItemWarehouses.Where(i => i.ItemID == addItem.ItemID && i.WarehouseID == addItem.WarehouseID).FirstOrDefault();
                    var item = db.Items.Where(i => i.ID == addItem.ItemID).FirstOrDefault();
                    if (itemWarehouse == null)
                    {
                        ItemWarehouse iw = AddItemWarehouse(addItem, item);
                        db.ItemWarehouses.Add(iw);
                    }
                    else
                    {
                        UpdateItemWarehouse(itemWarehouse, addItem, item, 1);
                       
                    }
                    db.Entry(oldAddItem).State = EntityState.Detached;
                    db.Entry(addItem).State = EntityState.Modified;
                    db.SaveChanges();
                    dbContextTransaction.Commit();
                    result = true;

                }
                catch (Exception err)
                {
                    // UpdateItemWarehouse(itemWarehouse, addItem, -1);
                    dbContextTransaction.Rollback();
                    result = false;
                }
            }
            return result;
        }

        public bool DeleteAddItem(FrunutStockEntities db, AddItem addItem)
        {
            bool result = false;
            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                var oldAddItem = db.AddItems.Find(addItem.ID);

                try
                {
                    if (oldAddItem == null)
                    {
                        //dbContextTransaction.Rollback();
                        //result = false;
                        throw new System.InvalidOperationException("Could't find old values.");
                    }

                    var oldItemWarehouse = db.ItemWarehouses.Where(i => i.ItemID == oldAddItem.ItemID && i.WarehouseID == oldAddItem.WarehouseID).FirstOrDefault();
                    var oldItem = db.Items.Where(i => i.ID == oldAddItem.ItemID).FirstOrDefault();
                    if (oldItemWarehouse == null)
                    {
                        throw new System.InvalidOperationException("Could't find old values.");
                    }

                    // remove old values
                    UpdateItemWarehouse(oldItemWarehouse, oldAddItem, oldItem, -1);
                    db.Entry(oldItemWarehouse).State = EntityState.Modified;
                    db.SaveChanges();
                                       
                    db.Entry(oldAddItem).State = EntityState.Detached;
                    db.Entry(addItem).State = EntityState.Deleted;
                    db.AddItems.Remove(addItem);
                    db.SaveChanges();
                    dbContextTransaction.Commit();
                    result = true;

                }
                catch (Exception err)
                {
                    // UpdateItemWarehouse(itemWarehouse, addItem, -1);
                    dbContextTransaction.Rollback();
                    result = false;
                }
            }
            return result;
        }

        public ItemWarehouse AddItemWarehouse(AddItem addItem, Item item)
        {
            ItemWarehouse iw = new ItemWarehouse();
            iw.ItemID = addItem.ItemID;
            iw.WarehouseID = addItem.WarehouseID;
            iw.QtyBoxesIn = addItem.QtyBoxes;
            iw.QtyKgIn = addItem.QtyKg;
            iw.QtyBoxesOnhand = addItem.QtyBoxes;
            iw.QtyKgOnhand = addItem.QtyKg;
            iw.QtyTotalWeightIn = addItem.QtyKg + (addItem.QtyBoxes * item.BoxWeight);
            iw.QtyTotalWeightOnhand = addItem.QtyKg + (addItem.QtyBoxes * item.BoxWeight);
            iw.QtyTotalWeightReserved = 0;
            return iw;
        }

        public void UpdateItemWarehouse(ItemWarehouse itemWarehouse, AddItem addItem, Item item, int factor)
        {
            itemWarehouse.QtyBoxesIn += addItem.QtyBoxes * factor;
            itemWarehouse.QtyKgIn += addItem.QtyKg * factor;
            itemWarehouse.QtyBoxesOnhand += addItem.QtyBoxes * factor;
            itemWarehouse.QtyKgOnhand += addItem.QtyKg * factor;
            itemWarehouse.QtyTotalWeightIn += (addItem.QtyKg * factor) + (addItem.QtyBoxes * item.BoxWeight) * factor;
            itemWarehouse.QtyTotalWeightOnhand += (addItem.QtyKg * factor) + (addItem.QtyBoxes * item.BoxWeight) * factor;
        }

        #endregion

        #region OrderDetails

        #endregion

    }
}
