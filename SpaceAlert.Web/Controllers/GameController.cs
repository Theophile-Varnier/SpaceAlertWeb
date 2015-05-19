using System.Web.Mvc;

namespace SpaceAlert.Web.Controllers
{
    public class GameController : Controller
    {
        // GET: Game
        public ActionResult Index()
        {
            return View();
        }
    }
}