using SpaceAlert.Model.Site;
using SpaceAlert.Model.Stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            return GetUniqueResult(m => m.Pseudo == pseudo && m.MotDePasse == motDePasse);
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
