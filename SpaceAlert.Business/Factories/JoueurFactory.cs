using SpaceAlert.Model.Helpers.Enums;
using SpaceAlert.Model.Jeu;
using SpaceAlert.Model.Plateau;
using System.Collections.Generic;

namespace SpaceAlert.Business.Factories
{
    public class JoueurFactory
    {
        public static Joueur CreateJoueur(long membreId, string nomPersonnage, bool captain, Vaisseau vaisseau)
        {
            return new Joueur
            {
                MembreId = membreId,
                NomPersonnage = nomPersonnage,
                IsCapitaine = captain,
                CurrentSalle = vaisseau.Salle(Zone.BLANCHE, Pont.HAUT),
                Actions = new Dictionary<int, ActionJoueur>
                {
                    {1, null},
                    {2, null},
                    {3, null},
                    {4, null},
                    {5, null},
                    {6, null},
                    {7, null},
                    {8, null},
                    {9, null},
                    {10, null},
                    {11, null},
                    {12, null}
                }
            };
        }
    }
}
