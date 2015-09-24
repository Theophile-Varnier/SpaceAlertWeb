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
        [Key, ForeignKey("MenaceInZone")]
        public int Id { get; set; }

        /// <summary>
        /// La menace associée
        /// </summary>
        public string MenaceName { get; set; }

        /// <summary>
        /// Gets or sets the menace in zone.
        /// </summary>
        /// <value>
        /// The menace in zone.
        /// </value>
        [Required]
        public virtual MenaceInZone MenaceInZone { get; set; }

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
