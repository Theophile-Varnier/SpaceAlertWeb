using SpaceAlert.Model.Helpers.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaceAlert.Model.Jeu
{
    [Table("Campagnes")]
    public class Campagne
    {
        [Key]
        public int Id { get; set; }

        public ICollection<Game> Games { get; set; }

        public Couleur Difficulte { get; set; }

        public int NbParties { get; set; }
    }
}
