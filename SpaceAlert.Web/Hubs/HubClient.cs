using Microsoft.AspNet.SignalR.Client;
using SpaceAlert.Business;
using SpaceAlert.Model.Jeu;
using SpaceAlert.Model.Jeu.Evenements;
using SpaceAlert.Web.Models;
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
        public static List<HubClient> Instances = new List<HubClient>();

        private readonly GameContext game;

        /// <summary>
        /// Gets the game identifier.
        /// </summary>
        /// <value>
        /// The game identifier.
        /// </value>
        public int GameId
        {
            get
            {
                return game.Id;
            }
        }

        private GameExecutionManager manager;

        private ServiceProvider serviceProvider;

        private IHubProxy hubProxy;

        /// <summary>
        /// Initializes a new instance of the <see cref="HubClient"/> class.
        /// </summary>
        /// <param name="gameId">The game identifier.</param>
        /// <param name="manager">The manager.</param>
        /// <param name="serviceProvider">The service provider.</param>
        public HubClient(int gameId, ServiceProvider serviceProvider)
        {
            game = serviceProvider.GameService.GetGameForExecution(gameId);
            this.serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Starts the asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task StartAsync()
        {
            Instances.Add(this);
            HubConnection hubConnection = new HubConnection(ConfigurationManager.AppSettings["SignalRServerUri"]);
            hubProxy = hubConnection.CreateHubProxy("PlayHub");
            await hubConnection.Start();
            manager = new GameExecutionManager(game.Game);
            manager.NewEventEvent += manager_NewEventEvent;
        }

        private void manager_NewEventEvent(object sender, NewEventArgs e)
        {
            string gameId = game.Id.ToString();
            if (e.Evenement is EvenementMenace)
            {
                CardViewModel model = new CardViewModel
                {
                    FrontImgUri = string.Concat(@"~/Content/Medias/", ((EvenementMenace)e.Evenement).MenaceName, ".png"),
                    BackImageUri = string.Concat(@"~/Content/Medias/", ((EvenementMenace)e.Evenement).Type, ".png")
                };
                hubProxy.Invoke("PopMenace", gameId, e.Evenement.GetType());
            }
            else
            {
                FinDePartie partie = e.Evenement as FinDePartie;
                if (partie != null)
                {
                    FinDePartie ev = partie;
                    manager.NewEventEvent -= manager_NewEventEvent;
                    hubProxy.Invoke("FinDePartie", gameId, ev.Phase);
                }
                else
                {
                    FinDePhase phase = e.Evenement as FinDePhase;
                    if (phase != null)
                    {
                        FinDePhase ev = phase;
                        hubProxy.Invoke("FinDePhase", gameId, ev.Phase);
                    }
                }
            }
        }

        private void Stop()
        {
            manager.NewEventEvent -= manager_NewEventEvent;
            manager.Stop();
        }

        /// <summary>
        /// Stops this instance.
        /// </summary>
        public static void Stop(int gameId)
        {
            HubClient selected = Instances.SingleOrDefault(i => i.GameId == gameId);
            if (selected != null)
            {
                selected.Stop();
                Instances.Remove(selected);
            }
        }
    }
}