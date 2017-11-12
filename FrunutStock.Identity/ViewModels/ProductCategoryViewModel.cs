using FrunutStock.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FrunutStock.Identity.ViewModels
{
    public class ProductCategoryViewModel
    {
        public int ProductCategoryID { get; set; }
        public string Name { get; set; }

        public List<ProductViewModel> Products { get; set; }
    }
}