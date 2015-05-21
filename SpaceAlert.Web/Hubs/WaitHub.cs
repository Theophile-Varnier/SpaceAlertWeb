using System;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;

namespace SpaceAlert.Web.Hubs
{
    public class WaitHub : Hub
    {
        public async Task Join(string charName, Guid gameId)
        {
            string id = gameId.ToString();
            await Groups.Add(Context.ConnectionId, id);
            Clients.Group(id).addChatMessage(charName + " joined.");
        }
        /// <summary>
        /// Quand un joueur rejoint une partie
        /// </summary>
        /// <param name="charName"></param>
        /// <param name="connectionId"></param>
        /// <param name="gameId"></param>
        /// <returns></returns>
        public static async Task JoinGame(string charName, string connectionId, Guid gameId)
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<WaitHub>();
            string id = gameId.ToString();
            await context.Groups.Add(connectionId, id);
            context.Clients.Group(id).addChatMessage(charName + " joined.");
        }
    }
}