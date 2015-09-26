using Microsoft.AspNet.SignalR;
using SpaceAlert.Business;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace SpaceAlert.Web.Hubs
{
    public class PlayHub : AbstractHub
    {
        private static ConcurrentDictionary<GameExecutionManager, string> games = new ConcurrentDictionary<GameExecutionManager, string>();

        public static async Task StartAsync(string gameId, GameExecutionManager manager)
        {
            games.AddOrUpdate(manager, gameId, (m, s) => gameId);
            manager.NewEventEvent += manager_NewEventEvent;
        }

        private static void manager_NewEventEvent(object sender, NewEventArgs e)
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<PlayHub>();
            string id = games[(GameExecutionManager)sender];

            context.Clients.Group(id).addChatMessage(e.Evenement.GetType());
        }
    }
}