using SpaceAlert.Model.Helpers.Enums;
using SpaceAlert.Model.Jeu;
using SpaceAlert.Model.Plateau;
using SpaceAlert.Model.Stats;
using System.Collections.Generic;

namespace SpaceAlert.Business.Factories
{
    public class JoueurFactory
    {
        public static Joueur CreateJoueur(Personnage perso, bool captain, Game game)
        {
            Joueur res = new Joueur
            {
                IdPersonnage = perso.Id,
                Personnage = perso,
                IsCapitaine = captain,
                CurrentSalle = new Position(Zone.BLANCHE, Pont.HAUT),
                Actions = new List<ActionInTour>()
            };
            return res;
        }
    }
}
