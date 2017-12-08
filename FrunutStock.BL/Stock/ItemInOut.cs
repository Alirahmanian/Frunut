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
        #region ReceiveItem Crud
        public bool CreateReceiveItem(FrunutStockEntities db, ReceiveItem receiveItem)
        {
            bool result = false;
            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                var itemWarehouse = db.ItemWarehouses.Where(i => i.ItemID == receiveItem.ItemID && i.WarehouseID == receiveItem.WarehouseID).FirstOrDefault();
                var item = db.Items.Where(i => i.ID == receiveItem.ItemID).FirstOrDefault();
                try
                {
                    if (itemWarehouse == null)
                    {
                       var iw = AddItemWarehouse(receiveItem, item);
                        db.ItemWarehouses.Add(iw);
                    }
                    else
                    {
                        UpdateItemWarehouse(itemWarehouse, receiveItem, item, 1);
                        db.Entry(itemWarehouse).State = EntityState.Modified;
                    }
                   
                    db.ReceiveItems.Add(receiveItem);
                    
                    db.SaveChanges();
                    dbContextTransaction.Commit();
                    result = true;
                }
                catch (Exception err)
                {
                    dbContextTransaction.Rollback();
                    result = false;
                }
            }
            return result;
        }

        public bool EditReceiveItem(FrunutStockEntities db, ReceiveItem receiveItem)
        {
            bool result = false;
            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                var oldReceiveItem = db.ReceiveItems.Find(receiveItem.ID);
                
                try
                {
                    if (oldReceiveItem == null)
                    {
                        throw new System.InvalidOperationException("Could't find old values.");
                    }

                    var oldItemWarehouse = db.ItemWarehouses.Where(i => i.ItemID == oldReceiveItem.ItemID && i.WarehouseID == oldReceiveItem.WarehouseID).FirstOrDefault();
                    var oldItem = db.Items.Where(i => i.ID == oldReceiveItem.ItemID).FirstOrDefault();
                    if (oldItemWarehouse == null)
                    {
                        throw new System.InvalidOperationException("Could't find old values.");
                    }

                        
                    UpdateItemWarehouse(oldItemWarehouse, oldReceiveItem, oldItem, -1);
                    db.Entry(oldItemWarehouse).State = EntityState.Modified;
                    db.SaveChanges();

                    var itemWarehouse = db.ItemWarehouses.Where(i => i.ItemID == receiveItem.ItemID && i.WarehouseID == receiveItem.WarehouseID).FirstOrDefault();
                    var item = db.Items.Where(i => i.ID == receiveItem.ItemID).FirstOrDefault();
                    if (itemWarehouse == null)
                    {
                        ItemWarehouse iw = AddItemWarehouse(receiveItem, item);
                        db.ItemWarehouses.Add(iw);
                    }
                    else
                    {
                        UpdateItemWarehouse(itemWarehouse, receiveItem, item, 1);
                       
                    }
                    db.Entry(oldReceiveItem).State = EntityState.Detached;
                    db.Entry(receiveItem).State = EntityState.Modified;
                    db.SaveChanges();
                    dbContextTransaction.Commit();
                    result = true;

                }
                catch (Exception err)
                {
                    dbContextTransaction.Rollback();
                    result = false;
                }
            }
            return result;
        }

        public bool DeleteReceiveItem(FrunutStockEntities db, ReceiveItem receiveItem)
        {
            bool result = false;
            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                var oldReceiveItem = db.ReceiveItems.Find(receiveItem.ID);

                try
                {
                    if (oldReceiveItem == null)
                    {
                        throw new System.InvalidOperationException("Could't find old values.");
                    }
                    var oldItemWarehouse = db.ItemWarehouses.Where(i => i.ItemID == oldReceiveItem.ItemID && i.WarehouseID == oldReceiveItem.WarehouseID).FirstOrDefault();
                    var oldItem = db.Items.Where(i => i.ID == oldReceiveItem.ItemID).FirstOrDefault();
                    if (oldItemWarehouse == null)
                    {
                        throw new System.InvalidOperationException("Could't find old values.");
                    }

                    // remove old values
                    UpdateItemWarehouse(oldItemWarehouse, oldReceiveItem, oldItem, -1);
                    db.Entry(oldItemWarehouse).State = EntityState.Modified;
                    db.SaveChanges();
                                       
                    db.Entry(oldReceiveItem).State = EntityState.Detached;
                    db.Entry(receiveItem).State = EntityState.Deleted;
                    db.ReceiveItems.Remove(receiveItem);
                    db.SaveChanges();
                    dbContextTransaction.Commit();
                    result = true;

                }
                catch (Exception err)
                {
                    dbContextTransaction.Rollback();
                    result = false;
                }
            }
            return result;
        }

        public ItemWarehouse AddItemWarehouse(ReceiveItem receiveItem, Item item)
        {
            ItemWarehouse iw = new ItemWarehouse();
            iw.ItemID = receiveItem.ItemID;
            iw.WarehouseID = receiveItem.WarehouseID;
            iw.QtyBoxesIn = receiveItem.QtyBoxes;
            iw.QtyKgIn = receiveItem.QtyKg;
            iw.QtyBoxesOnhand = receiveItem.QtyBoxes;
            iw.QtyKgOnhand = receiveItem.QtyKg;
            iw.QtyTotalWeightIn = receiveItem.QtyKg + (receiveItem.QtyBoxes * item.BoxWeight);
            iw.QtyTotalWeightOnhand = receiveItem.QtyKg + (receiveItem.QtyBoxes * item.BoxWeight);
            iw.QtyTotalWeightReserved = 0;
            return iw;
        }

        public void UpdateItemWarehouse(ItemWarehouse itemWarehouse, ReceiveItem receiveItem, Item item, int factor)
        {
            itemWarehouse.QtyBoxesIn += receiveItem.QtyBoxes * factor;
            itemWarehouse.QtyKgIn += receiveItem.QtyKg * factor;
            itemWarehouse.QtyBoxesOnhand += receiveItem.QtyBoxes * factor;
            itemWarehouse.QtyKgOnhand += receiveItem.QtyKg * factor;
            itemWarehouse.QtyTotalWeightIn += (receiveItem.QtyKg * factor) + (receiveItem.QtyBoxes * item.BoxWeight) * factor;
            itemWarehouse.QtyTotalWeightOnhand += (receiveItem.QtyKg * factor) + (receiveItem.QtyBoxes * item.BoxWeight) * factor;
        }

        #endregion

        #region OrderDetails

        #endregion

    }
}
