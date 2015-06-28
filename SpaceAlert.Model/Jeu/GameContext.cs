using SpaceAlert.Model.Helpers.Enums;
using SpaceAlert.Model.Menaces;

namespace SpaceAlert.Model.Jeu
{
    public class GameContext
    {
        /// <summary>
        /// L'état de la partie en cours
        /// </summary>
        public StatutPartie Statut { get; set; }

        /// <summary>
        /// L'ensemble des menaces disponibles pour la partie
        /// </summary>
        public ListOfMenaces MenacesDisponibles { get; set; }

        /// <summary>
        /// La partie en question
        /// </summary>
        public Game Partie { get; set; }

        /// <summary>
        /// Le tour actuel
        /// </summary>
        public int TourEnCours { get; set; }
    }
}
