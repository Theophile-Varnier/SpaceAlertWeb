using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceAlert.Model.Stats
{
    [Table("Hublot")]
    public class PointsDeHublot
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [Key]
        public int Id { get; set; }

        public virtual Statistiques Statistiques { get; set; }

        /// <summary>
        /// Gets or sets the phase.
        /// </summary>
        /// <value>
        /// The phase.
        /// </value>
        public int Phase { get; set; }

        /// <summary>
        /// Gets or sets the nb points.
        /// </summary>
        /// <value>
        /// The nb points.
        /// </value>
        public int NbPoints { get; set; }
    }
}
