using SpaceAlert.Model.Helpers.Enums;
using SpaceAlert.Model.Jeu;
using SpaceAlert.Model.Plateau;
using SpaceAlert.Model.Stats;
using System;
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
                CurrentSalle = new Position(Zone.Blanche, Pont.Haut),
                Actions = new List<ActionInTour>(),
                Deck = new List<PartialDeck>()
            };
            foreach(TypeAction type in Enum.GetValues(typeof(TypeAction)))
            {
                foreach (Direction direction in Enum.GetValues(typeof(Direction)))
                {
                    res.Deck.Add(new PartialDeck
                    {
                        NbCartes = 0,
                        TypeAction = type,
                        Mouvement = direction
                    });
                }
            }
            return res;
        }
    }
}
