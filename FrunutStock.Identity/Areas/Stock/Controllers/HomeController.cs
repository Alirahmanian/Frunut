﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FrunutStock.Identity.Areas.Stock.Controllers
{
    public class HomeController : Controller
    {
        // GET: Stock/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}