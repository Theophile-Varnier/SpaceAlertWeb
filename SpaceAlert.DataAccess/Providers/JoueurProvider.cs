using SpaceAlert.Model.Jeu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceAlert.DataAccess.Providers
{
    public class JoueurProvider: AbstractProvider<Joueur>
    {
        public JoueurProvider(SpaceAlertContext context)
            : base(context)
        {
            Table = context.Joueurs;
        }

        /// <summary>
        /// Enregistre une partie pour un joueur lorsque celle-ci est lancée
        /// </summary>
        public void RegisterGame(Joueur joueur, Game game)
        {
            joueur.GameId = game.Id;
            joueur.Personnage.Games.Add(joueur);
            context.SaveChanges();
        }
    }
}
