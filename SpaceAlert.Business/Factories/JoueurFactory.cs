using SpaceAlert.Model.Helpers.Enums;
using SpaceAlert.Model.Jeu;
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
                IsCapitaine = captain,
                CurrentSalle = game.Vaisseau.Salle(Zone.BLANCHE, Pont.HAUT),
                Actions = new Dictionary<int, ActionJoueur>()
            };
            return res;
        }
    }
}
