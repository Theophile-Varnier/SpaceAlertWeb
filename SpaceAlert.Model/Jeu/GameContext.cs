using System.Collections.Generic;
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

        /// <summary>
        /// Les menaces détruites au cours de la partie
        /// </summary>
        public List<Menace> MenacesDetruites { get; set; }

        /// <summary>
        /// Les menaces auxquelles le vaisseau a survécu
        /// </summary>
        public List<Menace> MenacesSurvecues { get; set; }

        /// <summary>
        /// Indique si la maintenance a été effectuée pendant cette phase
        /// </summary>
        public bool MaintenanceEffectuee { get; set; }
    }
}
