using SpaceAlert.Model.Helpers.Enums;

namespace SpaceAlert.Model.Jeu
{
    /// <summary>
    /// Représente une action en partie d'un joueur
    /// </summary>
    public class ActionJoueur
    {
        /// <summary>
        /// Le type de l'action
        /// </summary>
        public TypeAction TypeAction { get; set; }

        /// <summary>
        /// La valeur de l'action
        /// </summary>
        public int Value { get; set; }
    }
}
