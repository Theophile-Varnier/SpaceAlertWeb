using SpaceAlert.Model.Jeu;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaceAlert.Model.Stats
{
    [Table("PersonnagesInCampagnes")]
    public class PersonnageInCampagne
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Personnage")]
        public int PersonnageId { get; set; }

        public Personnage Personnage { get; set; }

        [ForeignKey("Campagne")]
        public int CampagneId { get; set; }

        public Campagne Campagne { get; set; }
    }
}
