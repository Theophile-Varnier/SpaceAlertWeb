﻿using SpaceAlert.Model.Menaces;

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
    }
}
