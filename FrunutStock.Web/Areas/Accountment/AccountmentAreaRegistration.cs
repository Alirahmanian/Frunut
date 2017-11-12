using System.Web.Mvc;

namespace FrunutStock.Web.Areas.Accountment
{
    public class AccountmentAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Accountment";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Accountment_default",
                "Accountment/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new[] { "FrunutStock.Web.Areas.Accountment.Controllers" }
            );
        }
    }
}