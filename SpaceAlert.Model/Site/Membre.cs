
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
        [Key]
        public long Id { get; set; }

        public string Pseudo { get; set; }

        public string Email { get; set; }

        public string MotDePasse { get; set; }

    }
}
