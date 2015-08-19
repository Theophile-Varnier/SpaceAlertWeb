using SpaceAlert.Model.Jeu.Evenements;
using SpaceAlert.Model.Menaces;

namespace SpaceAlert.Model.Jeu
{
    /// <summary>
    /// Représente une menace dans une partie en cours
    /// </summary>
    public class InGameMenace
    {

        /// <summary>
        /// La menace associée
        /// </summary>
        public Menace Menace { get; set; }

        /// <summary>
        /// La position actuelle de la menace
        /// </summary>
        public int Position { get; set; }

        /// <summary>
        /// Le tour d'arrivée de la menace
        /// </summary>
        public int TourArrive { get; set; }

        /// <summary>
        /// Les dégâts subis lors du tour en cours
        /// </summary>
        public int DegatsSubis { get; set; }

        /// <summary>
        /// Le nombre de pv actuel de la menace
        /// </summary>
        public int CurrentHp { get; set; }

        /// <summary>
        /// La vitesse actuelle de la menace
        /// </summary>
        public int CurrentSpeed { get; set; }

        /// <summary>
        /// La valeur actuelle de bouclier de la menace
        /// </summary>
        public int CurrentShield { get; set; }

        /// <summary>
        /// La rampe sur laquelle la menace se trouve
        /// </summary>
        public Rampe Rampe { get; set; }

        public int Distance
        {
            get
            {
                return (Rampe.NbCases - Position);
            }
        }

        public int Portee
        {
            get
            {
                return Distance / 5 + 1;
            }
        }
    }
}
