using SpaceAlert.Model.Helpers.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaceAlert.Model.Jeu
{
    /// <summary>
    /// Représente une action en partie d'un joueur
    /// </summary>
    [Table("Actions")]
    public class ActionJoueur
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
        /// Le type de l'action
        /// </summary>
        public GenreAction GenreAction { get; set; }

        /// <summary>
        /// La valeur de l'action
        /// </summary>
        public int Value { get; set; }
    }
}
