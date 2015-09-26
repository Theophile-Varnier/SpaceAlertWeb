using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;

namespace SpaceAlert.Web.Hubs
{
    public abstract class AbstractHub : Hub
    {
        /// <summary>
        /// Inscrit un membre à un groupe
        /// </summary>
        /// <param name="characterName"></param>
        /// <param name="gameId"></param>
        /// <returns></returns>
        public async Task JoinAsync(string characterName, string gameId)
        {
            await Groups.Add(Context.ConnectionId, gameId);

            Clients.OthersInGroup(gameId).addPlayer(characterName);
        }
    }
}