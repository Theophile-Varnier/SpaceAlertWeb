using SpaceAlert.Model.Helpers.Enums;

namespace SpaceAlert.Model.Jeu
{
    /// <summary>
    /// Représente une carte jouable
    /// </summary>
    public class PartialDeck
    {
        /// <summary>
        /// Gets or sets the nb cartes.
        /// </summary>
        /// <value>
        /// The nb cartes.
        /// </value>
        public int NbCartes { get; set; }

        /// <summary>
        /// Gets or sets the type action.
        /// </summary>
        /// <value>
        /// The type action.
        /// </value>
        public TypeAction TypeAction { get; set; }

        /// <summary>
        /// Gets or sets the mouvement.
        /// </summary>
        /// <value>
        /// The mouvement.
        /// </value>
        public Direction Mouvement { get; set; }
    }
}
