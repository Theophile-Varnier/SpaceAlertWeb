using SpaceAlert.Model.Stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceAlert.DataAccess.Providers
{
    public class PersonnageProvider: AbstractProvider<Personnage>
    {

        public PersonnageProvider(SpaceAlertContext context)
            : base(context)
        {
            Table = context.Set<Personnage>();
        }

        /// <summary>
        /// Gets the specified membre identifier.
        /// </summary>
        /// <param name="membreId">The membre identifier.</param>
        /// <param name="nomPersonnage">The nom personnage.</param>
        /// <returns></returns>
        public Personnage Get(long membreId, string nomPersonnage)
        {
            return GetUniqueResult(p => p.MembreId == membreId && p.Nom == nomPersonnage);
        }

        /// <summary>
        /// Adds the xp points.
        /// </summary>
        /// <param name="personnageId">The personnage identifier.</param>
        /// <param name="points">The points.</param>
        public void AddXpPoints(int personnageId, int points)
        {
            Table.Find(personnageId).Xp += points;
            context.SaveChanges();
        }
    }
}
