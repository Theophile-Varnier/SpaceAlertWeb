﻿using System;
using SpaceAlert.Model.Helpers.Enums;
using SpaceAlert.Model.Menaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaceAlert.Model.Jeu
{
    /// <summary>
    /// Représente une menace dans une partie en cours
    /// </summary>
    [Table("Menaces")]
    public class InGameMenace
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// La menace associée
        /// </summary>
        public string MenaceName { get; set; }

        /// <summary>
        /// Gets or sets the menace.
        /// </summary>
        /// <value>
        /// The menace.
        /// </value>
        [NotMapped]
        public Menace Menace { get; set; }

        /// <summary>
        /// Gets or sets the game identifier.
        /// </summary>
        /// <value>
        /// The game identifier.
        /// </value>
        [ForeignKey("Game")]
        public int GameId { get; set; }

        /// <summary>
        /// Gets or sets the game.
        /// </summary>
        /// <value>
        /// The game.
        /// </value>
        public virtual Game Game { get; set; }

        /// <summary>
        /// Gets or sets the zone.
        /// </summary>
        /// <value>
        /// The zone.
        /// </value>
        public Zone Zone { get; set; }

        /// <summary>
        /// Gets or sets the evenement.
        /// </summary>
        /// <value>
        /// The evenement.
        /// </value>
        public TimeSpan AnnonceEvenement { get; set; }

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
        public int RampeId { get; set; }
    }
}
