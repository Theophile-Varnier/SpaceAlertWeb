using System;
using System.Collections.Generic;

namespace SpaceAlert.Web.Models
{
    public class JoinGameViewModel: AbstractViewModel
    {
        public List<GameViewModel> AvailableGames { get; set; }

        public int GameToJoin { get; set; }

        public string ConnectionId { get; set; }

        public PlayerViewModel Player { get; set; }

        public override bool Validate()
        {
            throw new NotImplementedException();
        }
    }
}