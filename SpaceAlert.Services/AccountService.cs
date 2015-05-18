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
                using (DbTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        membreDao.EnregistrerMembre(membre, conn, tran);
                        tran.Commit();
                    }
                    catch (DaoException)
                    {
                        tran.Rollback();
                    }
                }
            }
        }
    }
}
