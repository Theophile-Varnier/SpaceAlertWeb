using SpaceAlert.Model.Jeu;
using System;
using System.Collections.Generic;

namespace SpaceAlert.Web.Models
{
    public class GameShipViewModel
    {
        public int GameId { get; set; }

        public string MissionId { get; set; }

        public List<string> Bonshommes { get; set; }

        public Dictionary<InGameZone, int> Energies { get; set; }

        public List<PlayerViewModel> Joueurs { get; set; }

        public List<SalleViewModel> Salles { get; set; }

        public int PhaseEnCours { get; set; }

        public CardViewModel CurrentMenace { get; set; }

        public List<CardViewModel> StartingDeck { get; set; }

        public PlayerViewModel ClientPlayer { get; set; }
    }
}