using System.Collections.Generic;
using SpaceAlert.Model.Helpers.Enums;
using SpaceAlert.Model.Plateau;

namespace SpaceAlert.Model.Jeu
{
    /// <summary>
    /// Représente un joueur pendant une partie en cours
    /// </summary>
    public class InGameJoueur
    {
        /// <summary>
        /// Le joueur associé
        /// </summary>
        public Joueur Joueur { get; set; }

        /// <summary>
        /// Ses actions
        /// </summary>
        public Dictionary<int, ActionJoueur> Actions { get; set; }

        /// <summary>
        /// A-t-il des robots avec lui ?
        /// </summary>
        public EtatRobots Robots { get; set; }

        /// <summary>
        /// La salle dans laquelle il se trouve actuellement
        /// </summary>
        public Salle CurrentSalle { get; set; }
    }
}
