using SpaceAlert.Model.Helpers.Enums;

namespace SpaceAlert.Model.Jeu
{
    /// <summary>
    /// Représente une carte jouable
    /// </summary>
    public class Carte
    {
        public TypeAction TypeAction { get; set; }

        public Direction Mouvement { get; set; }
    }
}
