using SpaceAlert.Model.Jeu;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceAlert.Model.Stats
{
    [Table("Statistiques")]
    public class Statistiques
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [Key, ForeignKey("Joueur")]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the joueur.
        /// </summary>
        /// <value>
        /// The joueur.
        /// </value>
        public virtual Joueur Joueur { get; set; }

        /// <summary>
        /// Gets or sets the degats.
        /// </summary>
        /// <value>
        /// The degats.
        /// </value>
        public List<Degats> Degats { get; set; }

        /// <summary>
        /// Gets or sets the points de hublot.
        /// </summary>
        /// <value>
        /// The points de hublot.
        /// </value>
        public List<PointsDeHublot> PointsDeHublot { get; set; }
    }
}
