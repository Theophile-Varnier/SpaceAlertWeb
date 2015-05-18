using System;
using SpaceAlert.DataAccess.Dao;
using SpaceAlert.DataAccess.Extensions;
using SpaceAlert.DataAccess.Repositories;
using SpaceAlert.Model.Site;
using System.Data.Common;

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
    }
}
