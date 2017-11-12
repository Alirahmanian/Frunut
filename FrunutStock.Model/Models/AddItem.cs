using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FrunutStock.Model.Models
{
    public class AddItem : BaseEntity
    {
        public DateTime Date { get; set; }
        public Int64 ItemID { get; set; }
        public Int64 WarehouseID { get; set; }
        public string Description { get; set; }
        public int QtyBoxes { get; set; }
        public decimal QtyKg { get; set; }
        public virtual Item Item { get; set; }
        public virtual Warehouse Warehouse { get; set; }
    }
}
