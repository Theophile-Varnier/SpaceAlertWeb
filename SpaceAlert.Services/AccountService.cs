using System;
using SpaceAlert.DataAccess.Dao;
using SpaceAlert.DataAccess.Extensions;
using SpaceAlert.DataAccess.Repositories;
using SpaceAlert.Model.Site;
using System.Data.Common;
using SpaceAlert.Services.Exceptions;

namespace SpaceAlert.Services
{
    public class AccountService
    {
        readonly MembreRepository membreRepository = new MembreRepository();

        /// <summary>
        /// Inscription d'un membre
        /// </summary>
        /// <param name="membre"></param>
        public void Inscrire(Membre membre)
        {
            membreRepository.EnregistrerMembre(membre);
        }

        /// <summary>
        /// Vérifie qu'un membre existe
        /// </summary>
        /// <param name="pseudo"></param>
        /// <returns></returns>
        public bool Existe(string pseudo)
        {

            return membreRepository.GetExistingMember(pseudo) != null;
        }

        /// <summary>
        /// Tente de se connecter
        /// </summary>
        /// <param name="pseudo"></param>
        /// <param name="motDePasse"></param>
        /// <returns></returns>
        public Membre RecupererMembre(string pseudo, string motDePasse)
        {
            return membreRepository.GetExistingMember(pseudo, motDePasse);
        }
    }
}
