using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrunutStock.Model.Models
{
   public class ItemGroup : BaseEntity
    {
        public string Name { get; set; }

        public virtual List<Item> Items { get; set; }
    }
}
