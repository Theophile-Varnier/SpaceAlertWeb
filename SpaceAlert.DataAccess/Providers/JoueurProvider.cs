using SpaceAlert.Model.Jeu;
using SpaceAlert.Model.Stats;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace SpaceAlert.DataAccess.Providers
{
    public class JoueurProvider: AbstractProvider<Joueur>
    {
        public JoueurProvider(SpaceAlertContext context)
            : base(context)
        {
            Table = context.Set<Joueur>();
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

        /// <summary>
        /// Gets the game characters.
        /// </summary>
        /// <param name="gameId">The game identifier.</param>
        /// <returns></returns>
        public IEnumerable<Personnage> GetGameCharacters(int gameId)
        {
            return Table
                .Include(j => j.Personnage)
                .Where(j => j.GameId == gameId)
                .Select(j => j.Personnage);
        }
    }
}
