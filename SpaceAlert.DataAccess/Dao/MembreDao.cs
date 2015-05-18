using SpaceAlert.DataAccess.Extensions;
using SpaceAlert.Model.Site;
using System.Data.Common;

namespace SpaceAlert.DataAccess.Dao
{
    public class MembreDao: BaseDao<Membre>
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

        public override Membre RecordToDto(DbDataReader reader)
        {
            return new Membre
            {
                Id = reader.GetInt64("ID"),
                Pseudo = reader.GetString("PSEUDO"),
                Email = reader.GetString("EMAIL")
            };
        }
    }
}
