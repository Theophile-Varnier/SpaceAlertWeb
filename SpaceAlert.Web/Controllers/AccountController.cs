using SpaceAlert.Model.Site;
using SpaceAlert.Services;
using SpaceAlert.Services.Exceptions;
using SpaceAlert.Web.Helpers;
using SpaceAlert.Web.Models;
using SpaceAlert.Web.Models.Mapping;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Security;

namespace SpaceAlert.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly ServiceProvider serviceProvider = new ServiceProvider();

        // GET: Account
        [Authorize]
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
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Connexion(AccountViewModel model, string returnUrl)
        {
            if (model.ErrorMessages == null)
            {
                model.ErrorMessages = new List<string>();
            }
            try
            {
                // On vérifie que les informations sont bonnes
                Membre membre = serviceProvider.AccountService.RecupererMembre(model.Pseudo, model.MotDePasse);

                // Si oui on renseigne les informations dans la session actuelle
                FormsAuthentication.SetAuthCookie(membre.Pseudo, false);

                // Gestion de la redirection depuis une page qui nécessite une authentification
                // Un peu crado mais pas trop le choix
                if (Request.UrlReferrer.GetParameter("ReturnUrl") != null)
                {
                    return Redirect(Request.UrlReferrer.GetParameter("ReturnUrl").Replace("%2f", "/"));
                }
                // Redirection par défaut
                return RedirectToAction("Index", "Game");
            }
            // Sinon on ajoute les messages d'erreurs et on laisse l'utilisateur retenter sa chacne
            catch (MembreNonExistantException mnee)
            {
                model.ErrorMessages.Add(mnee.Message);
            }
            catch (MotDePasseInvalideException mdpie)
            {
                model.ErrorMessages.Add(mdpie.Message);
            }
            finally
            {
                // On dégage le mot de passe pour le prochain essai
                model.MotDePasse = null;
            }
            return View(model);
        }

        /// <summary>
        /// Déconnecte la session en cours
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Deconnexion()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
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

            if (IsInvalid(model))
            {
                model.MotDePasse = null;
                model.Confirmation = null;
                return View(model);
            }
            // Si tout va bien on fait l'inscription
            Membre membre = AccountMapper.MapFromViewModel(model);
            serviceProvider.AccountService.Inscrire(membre);
            FormsAuthentication.SetAuthCookie(membre.Pseudo, false);
            return RedirectToAction("Index", "Game");
        }

        /// <summary>
        /// Vérifie si un modèle est apte à l'inscription
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private bool IsInvalid(AccountViewModel model)
        {
            bool res = !model.Validate();

            // On vérifie que l'adresse email n'est pas déjà utilisée
            if (serviceProvider.AccountService.EmailDejaUtilise(model.Email))
            {
                model.ErrorMessages.Add("Cette adresse est déjà utilisée.");
                res = true;
            }

            // On vérifie que le pseudo n'est pas déjà utilisé
            if (serviceProvider.AccountService.Existe(model.Pseudo))
            {
                model.ErrorMessages.Add("Ce pseudo est déjà utilisé.");
                res = true;
            }
            return res;
        }
    }
}