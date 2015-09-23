using SpaceAlert.Model.Helpers.Enums;
using SpaceAlert.Model.Plateau;
using SpaceAlert.Model.Stats;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaceAlert.Model.Jeu
{
    /// <summary>
    /// Représente un joueur d'une partie
    /// </summary>
    [Table("Joueurs")]
    public class Joueur
    {
        /// <summary>
        /// Id
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// L'id du personnage joué
        /// </summary>
        [ForeignKey("Personnage")]
        public int IdPersonnage { get; set; }

        public virtual Personnage Personnage { get; set; }

        /// <summary>
        /// La partie à laquelle est lié le joueur
        /// </summary>
        [ForeignKey("Game")]
        public Guid GameId { get; set; }

        public virtual Game Game { get; set; }

        /// <summary>
        /// La couleur du pion du joueur
        /// </summary>
        public string Couleur { get; set; }

        /// <summary>
        /// Indique si le joueur est le capitaine de l'équipe
        /// </summary>
        public bool IsCapitaine { get; set; }

        /// <summary>
        /// Ses actions
        /// </summary>
        public List<ActionInTour> Actions { get; set; }

        /// <summary>
        /// A-t-il des robots avec lui ?
        /// </summary>
        public EtatRobots Robots { get; set; }

        /// <summary>
        /// La salle dans laquelle il se trouve actuellement
        /// </summary>
        public Position CurrentSalle { get; set; }
    }
}
