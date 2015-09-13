using SpaceAlert.Model.Jeu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceAlert.DataAccess.Providers
{
    public class GameContextProvider: AbstractProvider<GameContext>
    {
        public GameContextProvider(SpaceAlertContext context)
            :base(context)
        {
            Table = context.Set<GameContext>();
        }
    }
}
