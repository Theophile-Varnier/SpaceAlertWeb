﻿using SpaceAlert.DataAccess;
using SpaceAlert.Model.Site;
using SpaceAlert.Model.Stats;
using SpaceAlert.Services.Exceptions;
using System;
using System.Drawing;
using System.IO;

namespace SpaceAlert.Business
{
    public class AccountService : AbstractService
    {
        public AccountService(UnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        /// <summary>
        /// Inscription d'un membre
        /// </summary>
        /// <param name="membre"></param>
        public void Inscrire(Membre membre)
        {
            unitOfWork.MembreProvider.Add(membre);
        }

        /// <summary>
        /// Vérifie qu'un membre existe
        /// </summary>
        /// <param name="pseudo"></param>
        /// <returns></returns>
        public bool Existe(string pseudo)
        {
            return unitOfWork.MembreProvider.GetMembreByPseudo(pseudo) != null;
        }

        /// <summary>
        /// Emails the deja utilise.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        public bool EmailDejaUtilise(string email)
        {
            return unitOfWork.MembreProvider.GetMailIfExists(email) != null;
        }

        /// <summary>
        /// Ajoute un personnage à un membre
        /// </summary>
        public void AddCharacter(long membreId, string charName)
        {
            Personnage newPersonnage = new Personnage
            {
                MembreId = membreId,
                Nom = charName,
                Clones = -1,
                Xp = 0
            };
            unitOfWork.MembreProvider.AddCharacter(membreId, newPersonnage);
        }

        /// <summary>
        /// Tente de se connecter
        /// </summary>
        /// <param name="pseudo"></param>
        /// <param name="motDePasse"></param>
        /// <returns></returns>
        public Membre RecupererMembre(string pseudo, string motDePasse)
        {
            if (!Existe(pseudo))
            {
                throw new MembreNonExistantException(string.Format("Le pseudo {0} n'existe pas", pseudo));
            }
            Membre res = unitOfWork.MembreProvider.GetMembreByPseudoAndMdp(pseudo, motDePasse);
            if (res == null)
            {
                throw new MotDePasseInvalideException("Combinaison pseudo/mot de passe invalide");
            }
            return res;
        }

        /// <summary>
        /// Récupère un membre à l'aide de son pseudo
        /// </summary>
        /// <param name="pseudo"></param>
        /// <returns></returns>
        public Membre RecupererMembre(string pseudo)
        {
            return unitOfWork.MembreProvider.GetMembreByPseudo(pseudo);
        }

        public Membre GetById(long id)
        {
            return unitOfWork.Context.Membres.Find(id);
        }
    }
}
