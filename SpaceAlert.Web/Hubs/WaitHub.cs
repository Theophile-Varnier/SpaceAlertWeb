using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace SpaceAlert.Web.Hubs
{
    public class WaitHub : Hub
    {

        private static ConcurrentDictionary<string, HubUser> GameUsers = new ConcurrentDictionary<string, HubUser>();

        private static ConcurrentDictionary<string, int> PlayersReady = new ConcurrentDictionary<string, int>();

        /// <summary>
        /// Inscrit un membre à un groupe
        /// </summary>
        /// <param name="charName"></param>
        /// <param name="gameId"></param>
        /// <returns></returns>
        public async Task Join(string charName, string gameId)
        {
            await Groups.Add(Context.ConnectionId, gameId);

            GameUsers.AddOrUpdate(charName, new HubUser
            {
                GameId = gameId,
                LastKnownConnectionId = Context.ConnectionId
            }, (s, user) =>
            {
                user.LastKnownConnectionId = Context.ConnectionId;
                return user;
            });

            Clients.OthersInGroup(gameId).addPlayer(charName);
        }

        /// <summary>
        /// Démarre une partie
        /// </summary>
        /// <param name="gameId"></param>
        /// <returns></returns>
        public async Task Start(string gameId)
        {
            await Clients.OthersInGroup(gameId).enableConnectionToGame();
            PlayerReady(gameId);
        }

        /// <summary>
        /// Notifie les clients qu'un joueur est prêt
        /// </summary>
        /// <param name="gameId">l'id de la partie</param>
        /// <returns></returns>
        public async Task PlayerReady(string gameId)
        {
            lock (PlayersReady)
            {
                PlayersReady.AddOrUpdate(gameId, 1, (s, i) => i + 1);
            }
            await Clients.Group(gameId).addPlayerReady(PlayersReady[gameId]);
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