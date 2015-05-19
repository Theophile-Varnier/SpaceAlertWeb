using SpaceAlert.DataAccess.Dao;
using SpaceAlert.DataAccess.Extensions;
using SpaceAlert.Model.Site;
using System.Data.Common;

namespace SpaceAlert.DataAccess.Repositories
{
    public class MembreRepository
    {
        private readonly MembreDao membreDao = new MembreDao();

        /// <summary>
        /// Enregistre un membre en base
        /// </summary>
        /// <param name="membre"></param>
        public void EnregistrerMembre(Membre membre)
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pseudo"></param>
        /// <param name="motDePasse"></param>
        /// <returns></returns>
        public Membre GetExistingMember(string pseudo, string motDePasse)
        {
            using (DbConnection conn = Command.GetConnexion())
            {
                conn.Open();
                try
                {
                    return membreDao.GetMembreByPseudoAndMdp(pseudo, motDePasse, conn);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pseudo"></param>
        /// <returns></returns>
        public Membre GetExistingMember(string pseudo)
        {
            using (DbConnection conn = Command.GetConnexion())
            {
                conn.Open();
                try
                {
                    return membreDao.GetMembreByPseudo(pseudo, conn);
                }
                finally
                {
                    conn.Close();
                }
            }
        }
    }
}
