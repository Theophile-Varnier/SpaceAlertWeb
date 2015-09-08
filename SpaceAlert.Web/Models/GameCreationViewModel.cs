
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace SpaceAlert.Web.Models
{
    public class GameCreationViewModel
    {
        [Display(Name="Personnage à utiliser")]
        public string CreatedBy { get; set; }

        public bool IsGameOwner { get; set; }

        public GameViewModel Game { get; set; }

        public IEnumerable<string> AvailableCharacters { get; set; }
    }
}