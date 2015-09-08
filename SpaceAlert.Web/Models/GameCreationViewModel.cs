
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace SpaceAlert.Web.Models
{
    public class GameCreationViewModel
    {
        public bool IsGameOwner { get; set; }

        public GameViewModel Game { get; set; }

        public PlayerViewModel Player { get; set; }
    }
}