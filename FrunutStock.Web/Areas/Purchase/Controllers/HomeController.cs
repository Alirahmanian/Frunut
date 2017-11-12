using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FrunutStock.Web.Areas.Purchase.Controllers
{
    public class HomeController : Controller
    {
        // GET: Purchase/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}