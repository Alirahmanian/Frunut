using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FrunutStock.Identity.ViewModels
{
    public class ProductViewModel
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public int ProductCategoryID { get; set; }
    }
}