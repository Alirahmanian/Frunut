using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FrunutStock.Identity.ViewModels
{
    public class ProductFormViewModel
    {
        public string ProductTitle { get; set; }
        public string ProductDescription { get; set; }
        public decimal ProductPrice { get; set; }
        public int ProductCategory { get; set; }
    }
}