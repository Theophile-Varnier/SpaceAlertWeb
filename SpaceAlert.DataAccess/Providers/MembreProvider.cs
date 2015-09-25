using SpaceAlert.Model.Site;
using SpaceAlert.Model.Stats;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace SpaceAlert.DataAccess.Providers
{
    public class MembreProvider : AbstractProvider<Membre>
    {
        public MembreProvider(SpaceAlertContext context)
            : base(context)
        {
            Table = context.Set<Membre>();
        }

        /// <summary>
        /// Vérifie si un membre existe
        /// </summary>
        public Membre GetMembreByPseudo(string pseudo)
        {
            return GetUniqueResult(m => m.Pseudo == pseudo);
        }

        /// <summary>
        /// Vérifie une combinaison Pseudo/Password
        /// </summary>
        public Membre GetMembreByPseudoAndMdp(string pseudo, string motDePasse)
        {
            byte[] array = Encoding.UTF8.GetBytes(motDePasse);
            SHA256Managed sha256 = new SHA256Managed();
            return GetUniqueResult(m => m.Pseudo == pseudo && m.PassWord == string.Join(string.Empty, sha256.ComputeHash(array).Select(b => string.Format("{0:x2}", b))));
        }

        public string GetMailIfExists(string email)
        {
            Membre membre = GetUniqueResult(m => m.Email == email);
            return membre != null ? membre.Email : null;
        }

        /// <summary>
        /// Ajoute un personnage à un membre
        /// </summary>
        public void AddCharacter(long membreId, Personnage personnage)
        {
            Membre membre = GetUniqueResult(m => m.Id == membreId);
            membre.Personnages.Add(personnage);
            context.SaveChanges();
        }
    }
}
