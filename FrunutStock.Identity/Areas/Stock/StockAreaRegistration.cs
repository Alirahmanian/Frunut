﻿using System.Web.Mvc;

namespace FrunutStock.Identity.Areas.Stock
{
    public class StockAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Stock";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Stock_default",
                "Stock/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }, new[] { "FrunutStock.Identity.Areas.Stock.Controllers" }
            );
        }
    }
}