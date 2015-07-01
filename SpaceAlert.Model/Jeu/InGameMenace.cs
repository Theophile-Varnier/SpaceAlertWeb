using SpaceAlert.Model.Menaces;

namespace SpaceAlert.Model.Jeu
{
    /// <summary>
    /// Représente une menace dans une partie en cours
    /// </summary>
    public class InGameMenace
    {
        public Menace Menace { get; set; }

        public int Position { get; set; }

        public int TourArrive { get; set; }

        /// <summary>
        /// Les dégâts subis lors du tour en cours
        /// </summary>
        public int DegatsSubis { get; set; }
    }
}
