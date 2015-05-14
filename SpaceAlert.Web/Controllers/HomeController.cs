using SpaceAlert.Model.Helpers.Enums;
using SpaceAlert.Model.Jeu;
using SpaceAlert.Model.Menaces;
using SpaceAlert.Model.Plateau;
using Spring.Context;
using Spring.Context.Support;
using System;
using System.Linq;
using System.Web.Mvc;

namespace SpaceAlert.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            IApplicationContext ctx = ContextRegistry.GetContext();
            Vaisseau vaisseau = (Vaisseau)ctx.GetObject("Vaisseau");
            Mission tuto = (Mission) ctx.GetObject("Tuto1");
            Menace menace = (Menace)ctx.GetObject("ArmoredGrappler");
            menace.ActionsX.First()(menace, vaisseau, TypeCase.X, Zone.ROUGE);
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