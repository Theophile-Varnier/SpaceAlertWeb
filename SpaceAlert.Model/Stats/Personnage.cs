using SpaceAlert.Model.Jeu;
using SpaceAlert.Model.Site;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaceAlert.Model.Stats
{
    /// <summary>
    /// Un Personnage de membre
    /// </summary>
    [Table("Personnages")]
    public class Personnage
    {
        /// <summary>
        /// Identifiant technique
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Id du membre
        /// </summary>
        [ForeignKey("Membre")]
        public long MembreId { get; set; }

        /// <summary>
        /// Membre associé
        /// </summary>
        public virtual Membre Membre { get; set; }

        /// <summary>
        /// Nom du personnage
        /// </summary>
        public string Nom { get; set; }

        /// <summary>
        /// L'expérience du personnage
        /// </summary>
        public int Xp { get; set; }

        /// <summary>
        /// Les parties du personnage
        /// </summary>
        public virtual ICollection<Joueur> Games { get; set; }

        /// <summary>
        /// Les campagnes du personnage
        /// </summary>
        public virtual ICollection<PersonnageInCampagne> Campagnes { get; set; }
    }
}
