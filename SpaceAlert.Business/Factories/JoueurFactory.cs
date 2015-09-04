using SpaceAlert.Model.Helpers.Enums;
using SpaceAlert.Model.Jeu;
using SpaceAlert.Model.Plateau;
using System.Collections.Generic;

namespace SpaceAlert.Business.Factories
{
    public class JoueurFactory
    {
        public static Joueur CreateJoueur(long membreId, string nomPersonnage, bool captain, Game game)
        {
            Joueur res = new Joueur
            {
                MembreId = membreId,
                NomPersonnage = nomPersonnage,
                IsCapitaine = captain,
                CurrentSalle = game.Vaisseau.Salle(Zone.BLANCHE, Pont.HAUT),
                Actions = new Dictionary<int, ActionJoueur>()
            };
            return res;
        }
    }
}
