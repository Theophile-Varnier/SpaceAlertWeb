using SpaceAlert.Model.Jeu;
using SpaceAlert.Services;
using SpaceAlert.Web.Models;
using SpaceAlert.Web.Models.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace SpaceAlert.Web.Controllers
{
    public class GameController : Controller
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
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Créé une partie
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
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
                    Name = model.CreatedBy
                }
            };

            // On initialise la partie côté serveur
            Guid gameId = serviceProvider.GameService.InitialiserGame(
                model.Game.TypeMission, 
                model.Game.NbJoueurs, 
                model.Game.Blanches, 
                model.Game.Jaunes, 
                model.Game.Rouges, 
                new List<string> { model.CreatedBy });

            model.Game.Players.First().Color = serviceProvider.GameService.PlayerColor(gameId, model.CreatedBy);

            // On renvoie vers la salle d'attente
            model.Game.GameId = gameId;
            model.IsGameOwner = true;
            return View("WaitRoom", model);
        }

        /// <summary>
        /// Salle d'attente
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult WaitRoom(GameCreationViewModel model)
        {
            return View(model);
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
                AvailableGames = new List<GameViewModel>()
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
            return serviceProvider.GameService.ProchaineCouleur(Guid.Parse(gameId), charName);
        }

        /// <summary>
        /// Ajoute un joueur à une partie
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Join(JoinGameViewModel model)
        {
            GameContext game = serviceProvider.GameService.GetGame(model.GameToJoin);
            if (game.Partie.Joueurs.Select(j => j.NomPersonnage).Contains(model.Player.Name))
            {
                model.ErrorMessages = new List<string>
                {
                    "Ce nom de personnage est déjà utilisé"
                };
                return View(model);
            }
            game.Partie.Joueurs.Add(new Joueur
            {
                NomPersonnage = model.Player.Name
            });
            serviceProvider.GameService.ProchaineCouleur(model.GameToJoin, model.Player.Name);
            GameCreationViewModel newModel = new GameCreationViewModel
            {
                CreatedBy = model.Player.Name,
                Game = GameMapper.MapToModel(game.Partie),
                IsGameOwner = false
            };
            return View("WaitRoom", newModel);
        }

        /// <summary>
        /// Page du jeu à proprement parler
        /// </summary>
        /// <param name="gameId"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Play(string gameId)
        {
            return View(ShipFactory.DefaultShip());
        }
    }
}