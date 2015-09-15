using SpaceAlert.Model.Helpers.Enums;
using SpaceAlert.Model.Jeu;

namespace SpaceAlert.Model.Plateau
{
    /// <summary>
    /// Décrit l'état d'une salle en cours de partie
    /// </summary>
    public class Salle
    {
        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        /// <value>
        /// The position.
        /// </value>
        public Position Position { get; set; }

        /// <summary>
        /// Le type d'action que génère le bouton C
        /// </summary>
        public ActionC ActionC { get; set; }

        /// <summary>
        /// Indique si la salle possède des robots
        /// </summary>
        public PresenceRobots HasRobots { get; set; }

        /// <summary>
        /// Le nombre max d'énergie (resource ou bouclier)
        /// </summary>
        public int EnergieMax { get; set; }

        /// <summary>
        /// Le nombre courant d'énergie (resource ou bouclier)
        /// </summary>
        public int EnergieCourante { get; set; }

        /// <summary>
        /// Le canon de la salle (pas le plus beau hein, le vrai canon)
        /// </summary>
        public Canon Canon { get; set; }
    }
}
