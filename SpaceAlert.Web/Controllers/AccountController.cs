using SpaceAlert.Services;
using SpaceAlert.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SpaceAlert.Web.Models.Mapping;

namespace SpaceAlert.Web.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Connexion()
        {
            return View();
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
            AccountService service = new AccountService();
            service.Inscrire(AccountMapper.MapFromViewModel(model));
            return View();
        }
    }
}