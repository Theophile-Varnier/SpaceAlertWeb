using System.Collections.Generic;
using SpaceAlert.Model.Helpers.Enums;
using SpaceAlert.Model.Plateau;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace SpaceAlert.Model.Jeu
{
    /// <summary>
    /// Décrit l'état d'une zone du vaisseau en Jeu
    /// </summary>
    [Table("Zones")]
    public class InGameZone
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
        /// Gets or sets the vaisseau identifier.
        /// </summary>
        /// <value>
        /// The vaisseau identifier.
        /// </value>
        [ForeignKey("Vaisseau")]
        public Guid VaisseauId { get; set; }

        /// <summary>
        /// Gets or sets the vaisseau.
        /// </summary>
        /// <value>
        /// The vaisseau.
        /// </value>
        public virtual Vaisseau Vaisseau { get; set; }

        /// <summary>
        /// Gets or sets the zone.
        /// </summary>
        /// <value>
        /// The zone.
        /// </value>
        public Zone Zone { get; set; }

        /// <summary>
        /// Les deux salles de la zone
        /// </summary>
        public List<Salle> Salles { get; set; }

        /// <summary>
        /// La rampe associée à cette zone
        /// </summary>
        public int RampeIndice { get; set; }

        /// <summary>
        /// Le nombre de dégâts subis par la zone
        /// </summary>
        public int Degats { get; set; }
    }
}
