using SpaceAlert.Business;
using SpaceAlert.Business.Exceptions;
using SpaceAlert.Model.Jeu;
using SpaceAlert.Model.Site;
using SpaceAlert.Web.Hubs;
using SpaceAlert.Web.Models;
using SpaceAlert.Web.Models.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace SpaceAlert.Web.Controllers
{
    [Authorize]
    public class GameController : AbstractController
    {
        private ServiceProvider serviceProvider = new ServiceProvider();

        // GET: Game
        /// <summary>
        /// Page d'accueil des parties
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Page de création d'une partie
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Create()
        {
            GameCreationViewModel viewModel = new GameCreationViewModel();
            Membre currentMember = serviceProvider.AccountService.RecupererMembre(User.Identity.Name);
            viewModel.Player = new PlayerViewModel
            {
                AvailableCharacters = currentMember.Personnages.Select(p => p.Nom)
            };
            return View(viewModel);
        }

        /// <summary>
        /// Créé une partie
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(GameCreationViewModel model)
        {
            if (!model.Game.Validate())
            {
                return View(model);
            }
            model.Game.Players = new List<PlayerViewModel>
            {
                new PlayerViewModel
                {
                    Name = model.Player.Name
                }
            };

            // On initialise la partie côté serveur
            try
            {
                Guid gameId = serviceProvider.GameService.InitialiserGame(
                    model.Game.TypeMission,
                    model.Game.NbJoueurs,
                    model.Game.Blanches,
                    model.Game.Jaunes,
                    model.Game.Rouges,
                    new KeyValuePair<long, string>(User.Id, model.Player.Name));

                model.Game.Players.First().Color = serviceProvider.GameService.GetPlayerColor(gameId, User.Id);

                // On renvoie vers la salle d'attente
                model.Game.GameId = gameId;
                model.IsGameOwner = true;
                TempData["GameModel"] = model;
                return RedirectToAction("WaitRoom");
            }
            catch (UserAlreadyInGameException)
            {
                return RedirectToAction("Create");
            }
        }

        /// <summary>
        /// Salle d'attente
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult WaitRoom()
        {
            GameCreationViewModel newModel = (GameCreationViewModel)TempData["GameModel"];
            if (newModel != null)
            {
                return View(newModel);
            }
            return RedirectToAction("Join");
        }

        /// <summary>
        /// Affiche la liste des parties disponibles
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Join()
        {
            JoinGameViewModel model = new JoinGameViewModel
            {
                AvailableGames = new List<GameViewModel>(),
                Player = new PlayerViewModel
                    {
                        AvailableCharacters = serviceProvider.AccountService.RecupererMembre(User.Identity.Name).Personnages.Select(p => p.Nom)
                    }
            };
            List<Game> games = serviceProvider.GameService.RecupererGameEnAttente();
            foreach (Game game in games)
            {
                model.AvailableGames.Add(GameMapper.MapToModel(game));
            }
            return View(model);
        }

        /// <summary>
        /// Change la couleur d'un joueur
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="charName"></param>
        /// <returns>Le nom de la nouvelle couleur</returns>
        [HttpGet]
        public string ChangeColor(string gameId, string charName)
        {
            return serviceProvider.GameService.GetNextColor(Guid.Parse(gameId), charName);
        }

        /// <summary>
        /// Ajoute un joueur à une partie
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Join(JoinGameViewModel model)
        {
            try
            {
                GameContext game = serviceProvider.GameService.AjouterJoueur(model.GameToJoin, User.Id, model.Player.Name);
                GameCreationViewModel newModel = new GameCreationViewModel
                {
                    Player = model.Player,
                    Game = GameMapper.MapToModel(game.Game),
                    IsGameOwner = false
                };
                TempData["GameModel"] = newModel;
                return RedirectToAction("WaitRoom");
            }
            catch (UserAlreadyInGameException)
            {
                model.ErrorMessages = new List<string>
                {
                    "T'es déjà dans une partie, espèce de gredin !"
                };
                return RedirectToAction("Join");
            }
            catch (PartiePleineException)
            {
                model.ErrorMessages = new List<string>
                {
                    "La partie est pleine, impossible de rejoindre la session."
                };
                return RedirectToAction("Join");
            }
        }

        /// <summary>
        /// Page du jeu à proprement parler
        /// </summary>
        /// <param name="gameId"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Play(Guid gameId)
        {
            serviceProvider.GameService.DemarrerGame(gameId);
            HubClient client = new HubClient(gameId, serviceProvider);
            client.StartAsync();
            return View(ShipFactory.DefaultShip(gameId));
        }

        /// <summary>
        /// Adds the player actions.
        /// </summary>
        /// <param name="gameId">The game identifier.</param>
        /// <param name="actions">The actions.</param>
        /// <returns></returns>
        public ActionResult AddPlayerActions(Guid gameId, ActionViewModel[] actions)
        {
            IEnumerable<ActionInTour> actionsToAdd = actions.Select(a => new ActionInTour
            {
                Action = new ActionJoueur{
                    GenreAction = a.Genre,
                    Value = a.Value
                },
                Tour = a.Tour
            });


            serviceProvider.GameService.AddPlayerActions(User.Id, gameId, actionsToAdd);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
    }
}