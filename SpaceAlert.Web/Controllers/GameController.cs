using System.Web.Mvc;
using SpaceAlert.Services;
using SpaceAlert.Web.Helpers;
using SpaceAlert.Web.Models;

namespace SpaceAlert.Web.Controllers
{
    public class GameController : Controller
    {
        private ServiceProvider serviceProvider = new ServiceProvider();

        // GET: Game
        /// <summary>
        /// Page d'accueil des parties
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Page de création d'une partie
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [CustomAuthorize]
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Créé une partie
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [CustomAuthorize]
        public ActionResult Create(GameViewModel model)
        {
            if (!model.Validate())
            {
                return View(model);
            }
            return View(model);
        }
    }
}