using SpaceAlert.DataAccess.Dao;
using SpaceAlert.DataAccess.Exceptions;
using SpaceAlert.DataAccess.Extensions;
using SpaceAlert.Model.Site;
using System.Data.Common;

namespace SpaceAlert.DataAccess.Repositories
{
    public class MembreRepository
    {
        private MembreDao membreDao = new MembreDao();

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
            Membre res;
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
                if (membreDao.GetMembreByPseudo(pseudo, conn) == null)
                {
                    throw new MembreNonExistantException(string.Format("Le pseudo {0} n'existe pas", pseudo));
                }
                if ((res = membreDao.GetMembreByPseudoAndMdp(pseudo, motDePasse, conn)) == null)
                {
                    throw new MotDePasseInvalideException("Combinaison pseudo/mot de passe invalide");
                }
            }
            return res;
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
