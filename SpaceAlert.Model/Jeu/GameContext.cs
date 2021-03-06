﻿using SpaceAlert.Model.Helpers.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaceAlert.Model.Jeu
{
    [Table("GameContext")]
    public class GameContext
    {
        [Key, ForeignKey("Game")]
        public int Id { get; set; }

        /// <summary>
        /// L'état de la partie en cours
        /// </summary>
        public StatutPartie Statut { get; set; }

        /// <summary>
        /// La partie en question
        /// </summary>
        public Game Game { get; set; }

        /// <summary>
        /// Le tour actuel
        /// </summary>
        public int TourEnCours { get; set; }

        /// <summary>
        /// Gets or sets the current phase.
        /// </summary>
        /// <value>
        /// The current phase.
        /// </value>
        public int CurrentPhase { get; set; }

        /// <summary>
        /// Gets or sets the deck.
        /// </summary>
        /// <value>
        /// The deck.
        /// </value>
        public List<PartialDeck> Deck { get; set; } 

        /// <summary>
        /// Les rampes
        /// </summary>
        public List<RampeInZone> Rampes { get; set; }

        /// <summary>
        /// La rampe interne
        /// </summary>
        public int RampeInterneId { get; set; }

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

        /// <summary>
        /// Gets or sets the configuration.
        /// </summary>
        /// <value>
        /// The configuration.
        /// </value>
        public GameConfig Config { get; set; }
    }
}
