using Microsoft.AspNet.SignalR.Client;
using SpaceAlert.Business;
using SpaceAlert.Model.Jeu;
using SpaceAlert.Model.Jeu.Evenements;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SpaceAlert.Web.Hubs
{
    /// <summary>
    /// 
    /// </summary>
    public class HubClient
    {
        private GameContext game;

        private GameExecutionManager manager;

        private ServiceProvider serviceProvider;

        private IHubProxy hubProxy;

        /// <summary>
        /// Initializes a new instance of the <see cref="HubClient"/> class.
        /// </summary>
        /// <param name="gameId">The game identifier.</param>
        /// <param name="manager">The manager.</param>
        /// <param name="serviceProvider">The service provider.</param>
        public HubClient(Guid gameId, ServiceProvider serviceProvider)
        {
            this.game = serviceProvider.GameService.GetGameForExecution(gameId);
            this.serviceProvider = serviceProvider;
        }

        public async Task StartAsync()
        {
            HubConnection hubConnection = new HubConnection(ConfigurationManager.AppSettings["SignalRServerUri"]);
            hubProxy = hubConnection.CreateHubProxy("PlayHub");
            await hubConnection.Start();
            manager = new GameExecutionManager(game.Game);
            manager.NewEventEvent += manager_NewEventEvent;
        }

        private void manager_NewEventEvent(object sender, NewEventArgs e)
        {
            string gameId = game.Id.ToString().ToUpperInvariant();
            if (e.Evenement is EvenementMenace)
            {
                hubProxy.Invoke("PopMenace", gameId, e.Evenement.GetType());
            }
            else if (e.Evenement is FinDePartie)
            {
                FinDePartie ev = (FinDePartie)e.Evenement;
                manager.NewEventEvent -= manager_NewEventEvent;
                hubProxy.Invoke("FinDePartie", gameId, ev.Phase);
            }
            else if (e.Evenement is FinDePhase)
            {
                FinDePhase ev = (FinDePhase)e.Evenement;
                hubProxy.Invoke("FinDePhase", gameId, ev.Phase);
            }
        }
    }
}