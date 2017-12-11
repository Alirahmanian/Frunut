using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FrunutStock.Model.Models
{
    public class OrderDetail : BaseEntity
    {
        
        public Int64 OrderID { get; set; }
        public Int64 ItemID { get; set; }
        public Int64 WarehouseID { get; set; }
        [DisplayFormat(DataFormatString = "{0:C0}", ApplyFormatInEditMode = true)]
        public decimal Price { get; set; }
        [DisplayFormat(DataFormatString = "{0:C0}", ApplyFormatInEditMode = true)]
        public decimal Extended_Price { get; set; }
        public int QtyBoxes { get; set; }
        public int QtyReservBoxes { get; set; }
        public decimal QtyKg { get; set; }

        //nav.
        public Order Order { get; set; }
        public Item Item { get; set; }
        public Warehouse Warehouse { get; set; }
    }
}
