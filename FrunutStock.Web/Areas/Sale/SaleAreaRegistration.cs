using System.Web.Http;
using System.Web.Mvc;

namespace FrunutStock.Web.Areas.Sale
{
    public class SaleAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Sale";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
           
            context.MapRoute(
                "Sale_default",
                "Sale/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new[] { "FrunutStock.Web.Areas.Sale.Controllers" }
            );
        }
    }
}