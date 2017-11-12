using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace FrunutStock.Model.Models
{
    public class Item : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal MinimumPrice { get; set; }
        public decimal BoxWeight { get; set; }
        public Int64 TotalBoxesOnHand { get; private set; }
        public decimal TotalWeightOnHand { get; private set; }
        public Int64 TotalBoxesReserved { get; private set; }
        public decimal TotalWeightReserved { get; private set; }

        public Int64 ItemGroupID { get; set; }
        [Display(Name = "Item group")]
        public ItemGroup ItemGroup { get; set; }
        public Int64 CountryID { get; set; }
        [Display(Name = "Country")]
        public Country Country { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }
        public ICollection<AddItem> AddItems { get; set; }
    }
  
}
