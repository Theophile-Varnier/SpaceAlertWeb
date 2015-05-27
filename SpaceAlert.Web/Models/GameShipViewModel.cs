using System.Collections.Generic;
using SpaceAlert.Model.Plateau;

namespace SpaceAlert.Web.Models
{
    public class GameShipViewModel
    {
        public List<string> Bonshommes { get; set; }

        public Dictionary<InGameZone, int> Energies { get; set; }

        public PlayerViewModel Joueur { get; set; }
    }
}