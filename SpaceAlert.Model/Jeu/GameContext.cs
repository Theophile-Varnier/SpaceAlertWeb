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
        /// Les rampes
        /// </summary>
        public Dictionary<Zone, Rampe> Rampes { get; set; }

        /// <summary>
        /// La rampe interne
        /// </summary>
        public Rampe RampeInterne { get; set; }

        /// <summary>
        /// Indique si la maintenance a été effectuée pendant cette phase
        /// </summary>
        public bool MaintenanceEffectuee { get; set; }

        /// <summary>
        /// Indique si des roquettes vont infliger des dégâts ce tour-ci
        /// </summary>
        public bool RoquettesThisTurn { get; set; }

        /// <summary>
        /// Indique si des roquettes ont été tirées ce tour-ci
        /// </summary>
        public bool RoquettesNextTurn { get; set; }
    }
}
