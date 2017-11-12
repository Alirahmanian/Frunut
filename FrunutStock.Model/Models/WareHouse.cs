using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FrunutStock.Model.Models
{
   public  class Warehouse :BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }
        public ICollection<AddItem> AddItems { get; set; }
    }
}
