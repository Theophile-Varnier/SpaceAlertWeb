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
        /// <summary>
        /// Le nom du personnage joué
        /// </summary>
        public string NomPersonnage { get; set; }

        /// <summary>
        /// La couleur du pion du joueur
        /// </summary>
        public string Couleur { get; set; }

        /// <summary>
        /// Indique si le joueur est le capitaine de l'équipe
        /// </summary>
        public bool IsCapitaine { get; set; }

        /// <summary>
        /// L'id du membre associé
        /// </summary>
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
