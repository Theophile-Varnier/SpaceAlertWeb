using SpaceAlert.Model.Helpers.Enums;
using SpaceAlert.Model.Jeu;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaceAlert.Model.Plateau
{
    /// <summary>
    /// Décrit l'état d'une salle en cours de partie
    /// </summary>
    [Table("Salles")]
    public class Salle
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
        /// Gets or sets the zone identifier.
        /// </summary>
        /// <value>
        /// The zone identifier.
        /// </value>
        [ForeignKey("Zone")]
        public int ZoneId { get; set; }

        /// <summary>
        /// Gets or sets the zone.
        /// </summary>
        /// <value>
        /// The zone.
        /// </value>
        public virtual InGameZone Zone { get; set; }


        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        /// <value>
        /// The position.
        /// </value>
        public Position Position { get; set; }

        /// <summary>
        /// Le type d'action que génère le bouton C
        /// </summary>
        public ActionC ActionC { get; set; }

        /// <summary>
        /// Indique si la salle possède des robots
        /// </summary>
        public PresenceRobots HasRobots { get; set; }

        /// <summary>
        /// Le nombre max d'énergie (resource ou bouclier)
        /// </summary>
        public int EnergieMax { get; set; }

        /// <summary>
        /// Le nombre courant d'énergie (resource ou bouclier)
        /// </summary>
        public int EnergieCourante { get; set; }

        /// <summary>
        /// Le canon de la salle (pas le plus beau hein, le vrai canon)
        /// </summary>
        public Canon Canon { get; set; }
    }
}
