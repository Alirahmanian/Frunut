using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace FrunutStock.Model.Models
{
    public class ItemWarehouse : BaseEntity
    {
        public Int64 ItemID { get; set; }
        public Int64 WarehouseID { get; set; }
        public int QtyBoxesIn { get; set; }
        public decimal QtyKgIn { get; set; }
        public decimal QtyTotalWeightIn { get; set; }
        public int QtyBoxesOnhand { get; set; }
        public decimal QtyKgOnhand { get; set; }
        public decimal QtyTotalWeightOnhand { get; set; }
        public int QtyBoxesReserved { get; set; }
        public decimal QtyTotalWeightReserved { get; set; }
        public virtual Item Item { get; set; }
        public virtual Warehouse Warehouse { get; set; }
    }
}
