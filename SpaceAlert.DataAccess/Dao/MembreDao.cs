using System.Collections.Generic;
using System.Linq;
using SpaceAlert.DataAccess.Extensions;
using SpaceAlert.Model.Site;
using System.Data.Common;

namespace SpaceAlert.DataAccess.Dao
{
    public class MembreDao : BaseDao<Membre>
    {
        /// <summary>
        /// Ajoute un nouveau membre
        /// </summary>
        /// <param name="membre">Le membre à ajouter</param>
        /// <param name="conn">La connection à utiliser</param>
        public void EnregistrerMembre(Membre membre, DbConnection conn)
        {
            string cmd = "INSERT INTO MEMBRE(PSEUDO, MDP, EMAIL)" +
                "VALUES(@pseudo, SHA2(@mdp, 256), @email)";
            ExecuteNonQuery(cmd, conn, (a) =>
            {
                a.AddWithValue("@pseudo", membre.Pseudo);
                a.AddWithValue("@mdp", membre.MotDePasse);
                a.AddWithValue("@email", membre.Email);
            });
        }

        /// <summary>
        /// Vérifie si un membre existe
        /// </summary>
        /// <param name="pseudo"></param>
        /// <param name="conn"></param>
        /// <returns></returns>
        public Membre GetMembreByPseudo(string pseudo, DbConnection conn)
        {
            string cmd = "SELECT * FROM MEMBRE WHERE PSEUDO = @pseudo";
            List<Membre> res = ExecuteReader(cmd, conn, (c) => c.AddWithValue("@pseudo", pseudo));
            return res.Any() ? res.First() : null;
        }

        /// <summary>
        /// Vérifie une combinaison Pseudo/Password
        /// </summary>
        /// <param name="pseudo"></param>
        /// <param name="motDePasse"></param>
        /// <param name="conn"></param>
        /// <returns></returns>
        public Membre GetMembreByPseudoAndMdp(string pseudo, string motDePasse, DbConnection conn)
        {
            string cmd = "SELECT * FROM MEMBRE WHERE PSEUDO = @pseudo AND MDP = SHA2(@mdp, 256)";
            List<Membre> res = ExecuteReader(cmd, conn, (c) =>
            {
                c.AddWithValue("@pseudo", pseudo);
                c.AddWithValue("@mdp", motDePasse);
            });
            return res.Any() ? res.First() : null;
        }

        public string GetEmailIfExists(string email, DbConnection conn)
        {
            string cmd = "SELECT EMAIL FROM MEMBRE WHERE EMAIL = @email";
            List<Membre> res = ExecuteReader(cmd, conn, (c) => c.AddWithValue("@email", email));
            return res.Any() ? res.First().Email : null;
        }

        public override Membre RecordToDto(DbDataReader reader)
        {
            return new Membre
            {
                Id = reader.GetInt64("ID"),
                Pseudo = reader.GetString("PSEUDO"),
                Email = reader.GetString("EMAIL"),
                MotDePasse = reader.GetString("MDP")
            };
        }
    }
}
