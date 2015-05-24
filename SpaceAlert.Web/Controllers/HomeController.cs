﻿using System.Web.Mvc;
using SpaceAlert.Web.Helpers;

namespace SpaceAlert.Web.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            if (Session.IsAuthenticated())
            {
                return RedirectToAction("Index", "Game");
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}