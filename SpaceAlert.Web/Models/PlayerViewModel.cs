
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace SpaceAlert.Web.Models
{
    public class PlayerViewModel
    {
        [Display(Name="Personnage à utiliser")]
        public string Name { get; set; }

        public string MembreName { get; set; }

        public int PersonnageId { get; set; }

        public string Avatar { get; set; }

        public string Color { get; set; }

        public IEnumerable<string> AvailableCharacters { get; set; }
    }
}