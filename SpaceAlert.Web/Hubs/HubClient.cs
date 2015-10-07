using Microsoft.AspNet.SignalR.Client;
using SpaceAlert.Business;
using SpaceAlert.Model.Helpers.Enums;
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
                    FrontImgUri = string.Concat(@"Content/Medias/", "Pulse_Ball.jpg"),
                    BackImageUri = string.Concat(@"Content/Medias/", "carte_dos", ".png")
                };
                hubProxy.Invoke("PopMenace", gameId, model.FrontImgUri, model.BackImageUri);
            }
            else if (e.Evenement is TransfertDeDonnees)
            {
                hubProxy.Invoke("DataTransfert", gameId);
            }
            else if (e.Evenement is DonneesEntrantes)
            {
                foreach (Joueur joueur in game.Game.Joueurs)
                {
                    Tuple<TypeAction, Direction> nextCard = serviceProvider.GameService.GetNextCard(GameId);
                    hubProxy.Invoke("TransfertCard", gameId, joueur.IdPersonnage.ToString(), nextCard.Item2, nextCard.Item1);
                }
            }
            else
            {
                FinDePartie partie = e.Evenement as FinDePartie;
                if (partie != null)
                {
                    FinDePartie ev = partie;
                    manager.NewEventEvent -= manager_NewEventEvent;
                    hubProxy.Invoke("FinDePartie", gameId, ev.Phase);
                    Instances.Remove(this);
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
            if (manager != null)
            {
                manager.NewEventEvent -= manager_NewEventEvent;
                manager.Stop();
            }
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