using SpaceAlert.Model.Helpers.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaceAlert.Model.Jeu
{
    [Table("RampesInZone")]
    public class RampeInZone
    {
        [ForeignKey("Game")]
        public int GameId { get; set; }

        public virtual GameContext Game { get; set; }

        [Key]
        public int Id { get; set; }

        public int RampeId { get; set; }

        public Zone Zone { get; set; }
    }
}
