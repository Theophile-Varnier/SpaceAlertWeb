using SpaceAlert.Model.Jeu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceAlert.DataAccess.Providers
{
    public class GameProvider: AbstractProvider<Game>
    {
        public GameProvider(SpaceAlertContext context)
            : base(context)
        {
            Table = context.Games;
        }
    }
}
