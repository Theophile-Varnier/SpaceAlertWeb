using SpaceAlert.Model.Jeu;
using SpaceAlert.Services;
using SpaceAlert.Web.Helpers;
using SpaceAlert.Web.Hubs;
using SpaceAlert.Web.Models;
using System.Collections.Generic;
using System.Web.Mvc;
using SpaceAlert.Web.Models.Mapping;

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
            Game game = GameMapper.MapFromModel(model.Game);
            GameContext context = serviceProvider.GameService.InitialiserGame(game);

            // On renvoie vers la salle d'attente
            model.Game.GameId = context.Partie.Id;
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
            List<Game> games = serviceProvider.GameService.RecupererGameEnCours();
            foreach (Game game in games)
            {
                model.AvailableGames.Add(GameMapper.MapToModel(game));
            }
            return View(model);
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
            game.Partie.Joueurs.Add(new Joueur
            {
                NomPersonnage = model.Player.Name
            });
            GameCreationViewModel newModel = new GameCreationViewModel
            {
                CreatedBy = model.Player.Name,
                Game = GameMapper.MapToModel(game.Partie),
                IsGameOwner = false
            };
            return View("WaitRoom", newModel);
        }
    }
}