
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using SpaceAlert.Model.Stats;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace SpaceAlert.Model.Site
{
    /// <summary>
    /// Membre du site
    /// </summary>
    [Table("Membres")]
    public class Membre
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the pseudo.
        /// </summary>
        /// <value>
        /// The pseudo.
        /// </value>
        public string Pseudo { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the pass word.
        /// </summary>
        /// <value>
        /// The pass word.
        /// </value>
        public string PassWord { get; private set; }

        [NotMapped]
        public string ClearPassWord
        {
            set
            {
                byte[] array = Encoding.UTF8.GetBytes(value);
                SHA256Managed sha256 = new SHA256Managed();
                PassWord = string.Join(string.Empty, sha256.ComputeHash(array).Select(b => string.Format("{0:x2}", b)));
            }
        }

        /// <summary>
        /// Gets or sets the personnages.
        /// </summary>
        /// <value>
        /// The personnages.
        /// </value>
        public virtual ICollection<Personnage> Personnages { get; set; }

        /// <summary>
        /// Gets or sets the current game.
        /// </summary>
        /// <value>
        /// The current game.
        /// </value>
        public Guid? CurrentGame { get; set; }

    }
}
