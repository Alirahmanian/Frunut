using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FrunutStock.Web.Areas.Accountment.Controllers
{
    public class HomeController : Controller
    {
        // GET: Accountment/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}