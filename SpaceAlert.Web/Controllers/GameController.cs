using System.Web.Mvc;
using SpaceAlert.Web.Helpers;
using SpaceAlert.Web.Models;

namespace SpaceAlert.Web.Controllers
{
    public class GameController : Controller
    {
        // GET: Game
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [CustomAuthorize]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(GameViewModel model)
        {
            return View(model);
        }
    }
}