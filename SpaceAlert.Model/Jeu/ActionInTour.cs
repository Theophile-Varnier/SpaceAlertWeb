using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaceAlert.Model.Jeu
{
    [Table("ActionsInTour")]
    public class ActionInTour
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
        /// Gets or sets the joueur identifier.
        /// </summary>
        /// <value>
        /// The joueur identifier.
        /// </value>
        [ForeignKey("Joueur")]
        public int JoueurId { get; set; }

        /// <summary>
        /// Gets or sets the joueur.
        /// </summary>
        /// <value>
        /// The joueur.
        /// </value>
        public virtual Joueur Joueur { get; set; }

        /// <summary>
        /// Gets or sets the tour.
        /// </summary>
        /// <value>
        /// The tour.
        /// </value>
        public int Tour { get; set; }

        /// <summary>
        /// Gets or sets the action.
        /// </summary>
        /// <value>
        /// The action.
        /// </value>
        public ActionJoueur Action { get; set; }
    }
}