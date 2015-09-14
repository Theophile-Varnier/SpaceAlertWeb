using System;
using System.Linq;
using SpaceAlert.Model.Helpers.Enums;
using System.Collections.Generic;
using SpaceAlert.Model.Jeu;

namespace SpaceAlert.Model.Plateau
{
    /// <summary>
    /// Décrit l'état du vaisseau à chaque instant de la partie
    /// </summary>
    public class Vaisseau
    {

        /// <summary>
        /// Le nombre de capsules d'énergie restant
        /// </summary>
        public int NbCapsules { get; set; }

        /// <summary>
        /// Le nombre de roquettes restant
        /// </summary>
        public int NbRoquettes { get; set; }

        /// <summary>
        /// Indique si les intercepteurs sont utilisés par un joueur
        /// </summary>
        public bool Interceptors { get; set; }

        /// <summary>
        /// L'état des différentes zones du vaisseau à chaque instant
        /// </summary>
        public Dictionary<Zone, InGameZone> Zones { get; set; }

        /// <summary>
        /// Indique si les robots ont été activés par un joueur
        /// </summary>
        public IList<bool> RobotsActifs { get; set; }


        /// <summary>
        /// Salles the specified p.
        /// </summary>
        /// <param name="p">The p.</param>
        /// <returns></returns>
        public Salle Salle(Position p)
        {
            return Zones[p.Zone].Salles[p.Pont];
        }

        /// <summary>
        /// Renvoie la salle dans laquelle un joueur arrive 
        /// après un mouvement
        /// </summary>
        /// <param name="source">La salle d'origine</param>
        /// <param name="direction">La direction du déplacement</param>
        /// <returns>La salle suivante</returns>
        public Salle SalleSuivante(Position source, Direction direction)
        {
            int minZone = (int)Enum.GetValues(typeof(Zone)).Cast<Zone>().Min();
            int maxZone = (int)Enum.GetValues(typeof(Zone)).Cast<Zone>().Max();

            // Ascenseur
            if ((int)direction == 0)
            {
                return Salle(new Position(source.Zone,(Pont)(1 - (int)source.Pont)));
            }
            // Mouvement latéral
            Position newPosition = new Position((Zone)Math.Min(Math.Max((int)source.Zone + (int)direction, minZone), maxZone), source.Pont);
            return Salle(newPosition);
        }
    }
}
