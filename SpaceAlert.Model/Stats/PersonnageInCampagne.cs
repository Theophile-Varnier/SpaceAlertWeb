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
