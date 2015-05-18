using System;
using SpaceAlert.DataAccess.Dao;
using SpaceAlert.DataAccess.Extensions;
using SpaceAlert.Model.Site;
using System.Data.Common;

namespace SpaceAlert.Services
{
    public class AccountService
    {

        MembreDao membreDao = new MembreDao();

        /// <summary>
        /// Inscription d'un membre
        /// </summary>
        /// <param name="membre"></param>
        public void Inscrire(Membre membre)
        {
            using (DbConnection conn = Command.GetConnexion())
            {
                conn.Open();
                try
                {
                    membreDao.EnregistrerMembre(membre, conn);
                }
                finally
                {
                    conn.Close();
                }
            }
        }
    }
}
