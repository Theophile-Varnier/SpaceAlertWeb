using SpaceAlert.Model.Helpers.Enums;
using Action = SpaceAlert.Model.Helpers.Enums.Action;

namespace SpaceAlert.Model.Jeu
{
    /// <summary>
    /// Représente une carte jouable
    /// </summary>
    public class Carte
    {
        public Action Action { get; set; }

        public Direction Mouvement { get; set; }
    }
}
