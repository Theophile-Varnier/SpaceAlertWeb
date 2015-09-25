using SpaceAlert.Business.Exceptions;
using SpaceAlert.Business.Factories;
using SpaceAlert.DataAccess;
using SpaceAlert.Model.Helpers;
using SpaceAlert.Model.Helpers.Enums;
using SpaceAlert.Model.Jeu;
using SpaceAlert.Model.Jeu.Evenements;
using SpaceAlert.Model.Menaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using SpaceAlert.Model.Site;

namespace SpaceAlert.Business
{
    public class GameService : AbstractService
    {

        public GameService(UnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }


        /// <summary>
        /// Initializes the game.
        /// </summary>
        /// <param name="typeMission">The type mission.</param>
        /// <param name="nbJoueurs">The nb joueurs.</param>
        /// <param name="blanches">if set to <c>true</c> [blanches].</param>
        /// <param name="jaunes">if set to <c>true</c> [jaunes].</param>
        /// <param name="rouges">if set to <c>true</c> [rouges].</param>
        /// <param name="captain">The captain.</param>
        /// <returns></returns>
        /// <exception cref="UserAlreadyInGameException"></exception>
        public Guid InitialiserGame(TypeMission typeMission, int nbJoueurs, bool blanches, bool jaunes, bool rouges, KeyValuePair<long, string> captain)
        {
            Membre membre = unitOfWork.MembreProvider.GetUniqueResult(m => m.Id == captain.Key);

            if (membre.CurrentGame != null)
            {
                throw new UserAlreadyInGameException();
            }

            GameContext res = GameFactory.CreateGame(typeMission, nbJoueurs, blanches, jaunes, rouges, unitOfWork.PersonnageProvider.Get(captain.Key, captain.Value));

            res.Game.Joueurs[0].Couleur = GetNextColor(res, res.Game.Joueurs[0].Personnage.Nom);
            membre.CurrentGame = res.Id;
            InitialiserRampes(res);
            unitOfWork.GameContextProvider.Add(res);

            unitOfWork.Context.SaveChanges();

            return res.Id;
        }


        /// <summary>
        /// Initializes the mission.
        /// </summary>
        /// <param name="game">The game.</param>
        private void InitialiserMission(GameContext game)
        {
            Dictionary<string, Mission> allMissions = SpaceAlertData.GetAll<Mission>();

            KeyValuePair<string, Mission> val = allMissions.Where(m => m.Value.TypeMission == game.Game.TypeMission).GetNextRandom();
            game.Game.MissionId = val.Key;
            Dictionary<string, Menace> availableMenaces = SpaceAlertData.GetAll<Menace>().Where(kvp => game.Game.Difficulte.HasFlag(kvp.Value.Couleur)).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            foreach (EvenementMenace evenement in game.Game.Mission.Evenements.OfType<EvenementMenace>())
            {
                KeyValuePair<string, Menace> selectedMenace = availableMenaces.GetNextRandom(kvp => kvp.Value.Type == evenement.Type);
                availableMenaces.Remove(selectedMenace.Key);
                evenement.MenaceName = selectedMenace.Key;
                game.Game.MenacesExternes.Add(MenaceFactory.CreateMenace(game, evenement));
            }
        }

        /// <summary>
        /// Démarre une partie
        /// </summary>
        /// <param name="gameId"></param>
        public void DemarrerGame(Guid gameId)
        {
            DemarrerGame(GetGame(gameId));
        }

        /// <summary>
        /// Démarrer une partie
        /// </summary>
        /// <param name="game"></param>
        /// <returns>L'id de la partie qui a été commencée</returns>
        public Guid DemarrerGame(GameContext game)
        {
            InitialiserMission(game);
            game.Statut = StatutPartie.JEU;
            game.TourEnCours = 1;

            foreach (Joueur joueur in game.Game.Joueurs)
            {
                for (int i = 1; i <= game.Game.Mission.NbTours; i++)
                {
                    joueur.Actions.Add(new ActionInTour
                    {
                        Tour = i,
                        JoueurId = joueur.Id,
                        Joueur = joueur,
                        Action = null
                    });
                }
            }
            RegisterGame(game.Game);
            unitOfWork.Context.SaveChanges();
            return game.Id;
        }

        /// <summary>
        /// Ajoute les rampes à une partie
        /// </summary>
        private void InitialiserRampes(GameContext game)
        {
            List<Rampe> allRampes = SpaceAlertData.GetAll<Rampe>().Select(kvp => kvp.Value).ToList();
            foreach (Zone zone in Enum.GetValues(typeof(Zone)))
            {
                game.Rampes.Add(new RampeInZone
                {
                    Zone = zone,
                    RampeId = allRampes.GetNextRandom(true).Id
                });
            }
            game.RampeInterneId = allRampes.GetNextRandom(true).Id;
        }

        /// <summary>
        /// Récupère toutes les parties en attente de joueurs
        /// </summary>
        /// <returns></returns>
        public List<Game> RecupererGameEnAttente()
        {
            IEnumerable<GameContext> contexts = unitOfWork.Context.GameContext
                .Include(g => g.Game)
                .Include(g => g.Game.Joueurs)
                .Where(g => g.Statut == StatutPartie.CREATION && g.Game.Joueurs.Count < g.Game.NbJoueurs).ToList();
            return contexts.Select(g => g.Game).ToList();
        }

        /// <summary>
        /// Récupère une game enregistrée
        /// </summary>
        /// <param name="gameId"></param>
        /// <returns></returns>
        public GameContext GetGame(Guid gameId)
        {
            return unitOfWork.Context.GameContext
                .Include(g => g.Game)
                .Include(g => g.Game.Joueurs)
                .SingleOrDefault(g => g.Id == gameId);
        }

        /// <summary>
        /// Gets the color of the player.
        /// </summary>
        /// <param name="gameId">The game identifier.</param>
        /// <param name="membreId">The membre identifier.</param>
        /// <returns></returns>
        public string GetPlayerColor(Guid gameId, long membreId)
        {
            // On récupère la partie en cours
            Game game = unitOfWork.Context.Games
                .Include(g => g.Joueurs)
                .Include(g => g.Joueurs.Select(j => j.Personnage))
                .Include(g => g.Joueurs.Select(j => j.Personnage).Select(p => p.Membre))
                .SingleOrDefault(g => g.Id == gameId);

            Joueur joueur = game != null ? game.Joueurs.FirstOrDefault(j => j.Personnage.Membre.Id == membreId) : null;
            return joueur != null ? joueur.Couleur : null;
        }

        /// <summary>
        /// Gets the color of the next.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <param name="characterName">Name of the character.</param>
        /// <returns></returns>
        public string GetNextColor(GameContext game, string characterName)
        {
            int index = SpaceAlertData.PlayerColors.IndexOf(game.Game.Joueurs.First(j => j.Personnage.Nom == characterName).Couleur);
            int current = index;
            string color;
            do
            {
                current = (current + 1) % SpaceAlertData.PlayerColors.Count;
                color = SpaceAlertData.PlayerColors[current];
            } while (current != index && game.Game.Joueurs.Select(j => j.Couleur).Contains(color));

            game.Game.Joueurs.First(j => j.Personnage.Nom == characterName).Couleur = color;

            unitOfWork.Context.SaveChanges();

            return color;
        }

        /// <summary>
        /// Gets the color of the next.
        /// </summary>
        /// <param name="gameId">The game identifier.</param>
        /// <param name="characterName">Name of the character.</param>
        /// <returns></returns>
        public string GetNextColor(Guid gameId, string characterName)
        {
            GameContext game = GetGame(gameId);
            return GetNextColor(game, characterName);
        }

        /// <summary>
        /// Ajoute un joueur à une partie
        /// </summary>
        /// <param name="gameId">La partie concernée</param>
        /// <param name="memberId">L'id du membre qui rejoint la partie</param>
        /// <param name="characterName">Le nom du personnage du membre</param>
        public GameContext AjouterJoueur(Guid gameId, long memberId, string characterName)
        {
            Membre membre = unitOfWork.MembreProvider.GetUniqueResult(j => j.Id == memberId);

            GameContext game = GetGame(gameId);

            // On vérifie que la partie n'est pas pleine.
            if (game.Game.Joueurs.Count == game.Game.NbJoueurs)
            {
                throw new PartiePleineException();
            }

            // On vérifie que le membre n'est pas déjà dans une partie (eh oh!)
            if (membre.CurrentGame != null)
            {
                throw new UserAlreadyInGameException();
            }

            membre.CurrentGame = gameId;

            // On ajoute le joueur à la partie
            game.Game.Joueurs.Add(JoueurFactory.CreateJoueur(unitOfWork.PersonnageProvider.Get(memberId, characterName), false, game.Game));

            // On lui assigne une couleur
            GetNextColor(gameId, characterName);

            unitOfWork.Context.SaveChanges();

            return game;
        }

        /// <summary>
        /// Enregistre une partie lorsqu'elle est lancée
        /// </summary>
        private void RegisterGame(Game game)
        {
            foreach (Joueur joueur in game.Joueurs)
            {
                unitOfWork.JoueurProvider.RegisterGame(joueur, game);
            }
            unitOfWork.GameProvider.Add(game);
        }
    }
}
