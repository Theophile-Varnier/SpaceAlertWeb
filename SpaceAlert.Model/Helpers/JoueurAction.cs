using SpaceAlert.Model.Helpers.Enums;
using SpaceAlert.Model.Jeu;
using SpaceAlert.Model.Plateau;
using System;
using System.Linq;

namespace SpaceAlert.Model.Helpers
{
    /// <summary>
    /// Représente les actions possibles d'un joueur
    /// </summary>
    /// TODO : Activation des robots, Utilisation des robots (cas particuliers, à gérer ailleurs ?)
    public static class JoueurAction
    {
        /// <summary>
        /// Tire sur une menace
        /// </summary>
        /// <param name="source">La salle d'où est effectuée l'action</param>
        /// <param name="partie">La partie en cours</param>
        public static void Shoot(Salle source, Game partie)
        {
            // Si le canon a déjà tiré, il ne se passe rien
            if (source.Canon.HasShot) return;

            source.Canon.HasShot = true;
            InGameMenace zoneMenace = partie.Menaces[source.Zone].OrderByDescending(m => m.Position).ThenBy(m => m.TourArrive).First();

            // S'il y a une menace dans la zone on lui inflige des dégâts
            if (zoneMenace != null)
            {
                int degats = Math.Max(0, source.Canon.Power - (zoneMenace.Menace.Shield - zoneMenace.DegatsSubis));
                zoneMenace.DegatsSubis += degats;
                zoneMenace.Menace.CurrentHp = Math.Max(0, zoneMenace.Menace.CurrentHp - degats);
                if (zoneMenace.Menace.CurrentHp == 0)
                {
                    partie.Menaces[source.Zone].Remove(zoneMenace);
                    partie.MenacesDetruites.Add(zoneMenace.Menace);
                }
            }

            // Si c'est un canon qui consomme de l'énergie, on la déduit de la réserve
            if (source.Canon.ConsumeEnergy)
            {
                partie.Vaisseau.Salle(source.Zone, Pont.BAS).EnergieCourante--;
            }
        }

        /// <summary>
        /// Charge les boucliers
        /// </summary>
        /// <param name="source">La salle d'où est effectuée l'action</param>
        /// <param name="partie">La partie en cours</param>
        public static void Shield(Salle source, Game partie)
        {
            // TODO : vérifier si on peut le faire plusieurs fois
            int incrValue = Math.Min(source.EnergieMax - source.EnergieCourante, partie.Vaisseau.Salle(source.Zone, Pont.BAS).EnergieCourante);
            source.EnergieCourante += incrValue;
            partie.Vaisseau.Salle(source.Zone, Pont.BAS).EnergieCourante -= incrValue;
        }

        /// <summary>
        /// Charge les réacteurs
        /// </summary>
        /// <param name="source">La salle d'où est effectuée l'action</param>
        /// <param name="partie">La partie en cours</param>
        public static void FillEnergy(Salle source, Game partie)
        {
            // Cas de la salle centrale du bas
            if (source.Zone == Zone.BLANCHE && source.Pont == Pont.BAS && partie.Vaisseau.NbCapsules > 0)
            {
                partie.Vaisseau.NbCapsules--;
                source.EnergieCourante = source.EnergieMax;
            }
            else
            {
                int incrValue = Math.Min(partie.Vaisseau.Salle(Zone.BLANCHE, Pont.BAS).EnergieCourante, source.EnergieMax - source.EnergieCourante);
                partie.Vaisseau.Salle(Zone.BLANCHE, Pont.BAS).EnergieCourante -= incrValue;
                source.EnergieCourante += incrValue;
            }
        }

        /// <summary>
        /// Tire des roquettes
        /// </summary>
        /// <param name="source">La salle d'où est effectuée l'action</param>
        /// <param name="partie">La partie en cours</param>
        public static void ShootRocket(Salle source, Game partie)
        {
            if (partie.Vaisseau.NbRoquettes > 0)
            {
                partie.Vaisseau.NbRoquettes--;
            }
        }

        /// <summary>
        /// Active les intercepteurs
        /// </summary>
        /// <param name="source">La salle d'où est effectuée l'action</param>
        /// <param name="partie">La partie en cours</param>
        public static void ActivateInterceptors(Salle source, Game partie)
        {
            partie.Vaisseau.Interceptors = true;
        }

        /// <summary>
        /// Fais la maintenance
        /// </summary>
        /// <param name="source">La salle d'où est effectuée l'action</param>
        /// <param name="partie">La partie en cours</param>
        public static void Maintenance(Salle source, Game partie)
        {
            partie.Vaisseau.MaintenanceEffectuee = true;
        }
    }
}
