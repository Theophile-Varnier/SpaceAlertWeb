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

        public Personnage Get(long membreId, string nomPersonnage)
        {
            return GetUniqueResult(p => p.MembreId == membreId && p.Nom == nomPersonnage);
        }
    }
}
