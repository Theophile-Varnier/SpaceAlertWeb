using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace SpaceAlert.Web.Hubs
{
    public class WaitHub : Hub
    {

        private static ConcurrentDictionary<string, HubUser> GameUsers = new ConcurrentDictionary<string, HubUser>();

        /// <summary>
        /// Inscrit un membre à un groupe
        /// </summary>
        /// <param name="charName"></param>
        /// <param name="gameId"></param>
        /// <returns></returns>
        public async Task Join(string charName, string gameId)
        {
            await Groups.Add(Context.ConnectionId, gameId);
            Clients.OthersInGroup(gameId).addChatMessage(charName);
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

            if (GameUsers.ContainsKey(charName))
            {
                if (GameUsers[charName].GameId != gameId)
                {
                    throw new UserAlreadyInGameException();
                }
                await context.Groups.Remove(GameUsers[charName].LastKnownConnectionId, id);
            }
            GameUsers.AddOrUpdate(charName, _ => new HubUser
            {
                GameId = gameId,
                LastKnownConnectionId = connectionId
            }, (c, u) =>
            {
                u.LastKnownConnectionId = connectionId;
                return u;
            });

            await context.Groups.Add(connectionId, id);

            context.Clients.Group(id).addChatMessage(charName + " joined.");
        }
    }
}