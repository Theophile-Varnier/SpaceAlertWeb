using SpaceAlert.Model.Helpers.Enums;
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
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public MenaceStatus Status { get; set; }

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

        /// <summary>
        /// Gets the distance.
        /// </summary>
        /// <value>
        /// The distance.
        /// </value>
        public int Distance
        {
            get
            {
                return (Rampe.NbCases - Position);
            }
        }

        /// <summary>
        /// Gets the portee.
        /// </summary>
        /// <value>
        /// The portee.
        /// </value>
        public int Portee
        {
            get
            {
                return Distance / 5 + 1;
            }
        }
    }
}
