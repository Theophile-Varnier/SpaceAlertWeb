using SpaceAlert.Model.Jeu;
using System.Collections.Generic;

namespace SpaceAlert.Web.Models
{
    public class GameShipViewModel
    {
        public List<string> Bonshommes { get; set; }

        public Dictionary<InGameZone, int> Energies { get; set; }

        public PlayerViewModel Joueur { get; set; }

        public List<SalleViewModel> Salles { get; set; } 

    }
}