using SpaceAlert.Model.Helpers.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaceAlert.Model.Jeu
{
    [Table("MenacesInZone")]
    public class MenaceInZone
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Game")]
        public Guid GameId { get; set; }

        public virtual Game Game { get; set; }

        public Zone Zone { get; set; }

        public InGameMenace Menace { get; set; }
    }
}