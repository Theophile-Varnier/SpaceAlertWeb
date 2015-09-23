using SpaceAlert.Model.Helpers.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaceAlert.Model.Jeu
{
    [Table("MenacesInZone")]
    public class MenaceInZone
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
        /// Gets or sets the game identifier.
        /// </summary>
        /// <value>
        /// The game identifier.
        /// </value>
        [ForeignKey("Game")]
        public Guid GameId { get; set; }

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
        /// Gets or sets the menace.
        /// </summary>
        /// <value>
        /// The menace.
        /// </value>
        public InGameMenace Menace { get; set; }

        /// <summary>
        /// Gets or sets the evenement.
        /// </summary>
        /// <value>
        /// The evenement.
        /// </value>
        public TimeSpan AnnonceEvenement { get; set; }
    }
}