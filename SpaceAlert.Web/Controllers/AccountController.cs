using SpaceAlert.Services;
using SpaceAlert.Web.Models;
using SpaceAlert.Web.Models.Mapping;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SpaceAlert.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly ServiceProvider serviceProvider = new ServiceProvider();
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Envoie vers la page de connexion
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Connexion()
        {
            return View();
        }

        /// <summary>
        /// Se connecte à l'application
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Connexion(AccountViewModel model)
        {

            model.MotDePasse = null;
            return View(model);
        }

        /// <summary>
        /// Envoie vers la page d'inscription
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Inscription()
        {
            return View();
        }

        /// <summary>
        /// Inscription d'un nouveau membre
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Inscription(AccountViewModel model)
        {
            if (model.ErrorMessages == null)
            {
                model.ErrorMessages = new List<string>();
            }

            // On vérifie que la confirmation du mot de passe est bonne
            if (model.Confirmation != model.MotDePasse)
            {
                model.ErrorMessages.Add("Confirmation incorrecte.");
            }

            // On vérifie que le pseudo n'est pas déjà utilisé
            if (serviceProvider.AccountService.Existe(model.Pseudo))
            {
                model.ErrorMessages.Add("Ce pseudo est déjà utilisé.");
            }
            else
            {
                serviceProvider.AccountService.Inscrire(AccountMapper.MapFromViewModel(model));
            }
            model.MotDePasse = null;
            model.Confirmation = null;
            return View(model);
        }
    }
}