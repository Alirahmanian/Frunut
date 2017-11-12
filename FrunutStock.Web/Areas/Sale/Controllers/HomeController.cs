using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FrunutStock.Web.Areas.Sale.Controllers
{
    public class HomeController : Controller
    {
        // GET: Sale/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}