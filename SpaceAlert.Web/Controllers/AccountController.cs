﻿using SpaceAlert.Business;
using SpaceAlert.Model.Site;
using SpaceAlert.Services.Exceptions;
using SpaceAlert.Web.Common;
using SpaceAlert.Web.Helpers;
using SpaceAlert.Web.Models;
using SpaceAlert.Web.Models.Mapping;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace SpaceAlert.Web.Controllers
{
    public class AccountController : AbstractController
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
        [AllowAnonymous]
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

                CreateAuthenticationTicket(membre.Pseudo);

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
        [AllowAnonymous]
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
        [AllowAnonymous]
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
            using (Image image = Image.FromFile(Server.MapPath("~/Content/Medias/default.jpg")))
            {
                using (MemoryStream m = new MemoryStream())
                {
                    image.Save(m, image.RawFormat);
                    byte[] imageBytes = m.ToArray();

                    // Convert byte[] to Base64 String
                    membre.Avatar = Convert.ToBase64String(imageBytes);
                }
            }
            serviceProvider.AccountService.Inscrire(membre);
            FormsAuthentication.SetAuthCookie(membre.Pseudo, false);
            CreateAuthenticationTicket(membre.Pseudo);
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

        [HttpPost]
        [Authorize]
        public string AddCharacter(string charName)
        {
            serviceProvider.AccountService.AddCharacter(User.Id, charName);
            return charName;
        }

        public void CreateAuthenticationTicket(string userName)
        {
            Membre authUser = serviceProvider.AccountService.RecupererMembre(userName);
            CustomPrincipalSerializedModel serializeModel = new CustomPrincipalSerializedModel();

            serializeModel.Id = authUser.Id;
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string userData = serializer.Serialize(serializeModel);

            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
              1, userName, DateTime.Now, DateTime.Now.AddHours(8), false, userData);
            string encTicket = FormsAuthentication.Encrypt(authTicket);
            HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
            Response.Cookies.Add(faCookie);
        }
    }
}