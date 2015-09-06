using SpaceAlert.Model.Jeu;
using SpaceAlert.Model.Site;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public int Id { get; set; }

        /// <summary>
        /// Id du membre
        /// </summary>
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

        public virtual ICollection<Campagne> Campagnes { get; set; }
    }
}
