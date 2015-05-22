using System;
using System.Collections.Generic;

namespace SpaceAlert.Web.Models
{
    public class JoinGameViewModel
    {
        public List<GameViewModel> AvailableGames { get; set; }

        public Guid GameToJoin { get; set; }

        public string ConnectionId { get; set; }

        public PlayerViewModel Player { get; set; }
    }
}