﻿using System.Web.Mvc;

namespace FrunutStock.Identity.Areas.Management
{
    public class ManagementAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Management";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Management_default",
                "Management/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }, new[] { "FrunutStock.Identity.Areas.Management.Controllers" }
            );
        }
    }
}