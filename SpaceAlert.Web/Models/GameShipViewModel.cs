using SpaceAlert.Model.Jeu;
using System;
using System.Collections.Generic;

namespace SpaceAlert.Web.Models
{
    public class GameShipViewModel
    {
        public Guid GameId { get; set; }

        public List<string> Bonshommes { get; set; }

        public Dictionary<InGameZone, int> Energies { get; set; }

        public PlayerViewModel Joueur { get; set; }

        public List<SalleViewModel> Salles { get; set; }

        public int PhaseEnCours { get; set; }

    }
}