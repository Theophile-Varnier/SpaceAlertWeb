using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace SpaceAlert.Web.Hubs
{
    public class WaitHub : AbstractHub
    {
        private static ConcurrentDictionary<string, int> PlayersReady = new ConcurrentDictionary<string, int>();

        /// <summary>
        /// Starts the asynchronous.
        /// </summary>
        /// <param name="gameId">The game identifier.</param>
        /// <returns></returns>
        public async Task StartAsync(string gameId)
        {
            await Clients.OthersInGroup(gameId).enableConnectionToGame();
            PlayerReadyAsync(gameId);
        }

        /// <summary>
        /// Notifie les clients qu'un joueur est prêt
        /// </summary>
        /// <param name="gameId">l'id de la partie</param>
        /// <returns></returns>
        public async Task PlayerReadyAsync(string gameId)
        {
            lock (PlayersReady)
            {
                PlayersReady.AddOrUpdate(gameId, 1, (s, i) => i + 1);
            }
            await Clients.Group(gameId).addPlayerReady(PlayersReady[gameId]);
        }


        /// <summary>
        /// Notifies the color changed asynchronous.
        /// </summary>
        /// <param name="gameId">The game identifier.</param>
        /// <param name="oldColor">The old color.</param>
        /// <param name="newColor">The new color.</param>
        /// <returns></returns>
        public async Task NotifyColorChangedAsync(string gameId, string oldColor, string newColor)
        {
            await Clients.Group(gameId).notifyColorChanged(oldColor, newColor);
        }
    }
}