using SpaceAlert.Model.Helpers.Enums;
using SpaceAlert.Model.Plateau;
using System.Collections.Generic;

namespace SpaceAlert.Model.Jeu
{
    /// <summary>
    /// Représente un joueur d'une partie
    /// </summary>
    public class Joueur
    {
        public string NomPersonnage { get; set; }

        public string Couleur { get; set; }

        public bool IsCapitaine { get; set; }

        public long MembreId { get; set; }

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
