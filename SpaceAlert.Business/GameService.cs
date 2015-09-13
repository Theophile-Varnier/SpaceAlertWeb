using SpaceAlert.Business.Exceptions;
using SpaceAlert.Business.Factories;
using SpaceAlert.DataAccess;
using SpaceAlert.Model.Helpers;
using SpaceAlert.Model.Helpers.Enums;
using SpaceAlert.Model.Jeu;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace SpaceAlert.Business
{
    public class GameService : AbstractService
    {

        public GameService(UnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        /// <summary>
        /// Initialise une nouvelle partie
        /// </summary>
        /// <param name="typeMission">Le type de mission pour la partie</param>
        /// <param name="nbJoueurs">Le nombre de joueurs attendus</param>
        /// <param name="blanches">menaces blanches</param>
        /// <param name="jaunes">menaces jaunes</param>
        /// <param name="rouges">menaces rouges</param>
        /// <param name="playerNames">Le nom des personnages des joueurs</param>
        /// <returns></returns>
        public Guid InitialiserGame(TypeMission typeMission, int nbJoueurs, bool blanches, bool jaunes, bool rouges, KeyValuePair<long, string> captain)
        {
            GameContext res = GameFactory.CreateGame(typeMission, nbJoueurs, blanches, jaunes, rouges, unitOfWork.PersonnageProvider.Get(captain.Key, captain.Value));
            InitialiserRampes(res);
            unitOfWork.GameContextProvider.Add(res);

            return res.Game.Id;
        }

        /// <summary>
        /// Récupère une mission aléatoire d'un type défini
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private Mission InitialiserMission(TypeMission type)
        {
            Dictionary<string, Mission> allMissions = SpaceAlertData.GetAll<Mission>();

            return allMissions.Where(m => m.Value.TypeMission == type).GetNextRandom().Value;
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
            game.Game.Mission = InitialiserMission(game.Game.TypeMission);
            game.Statut = StatutPartie.JEU;
            game.TourEnCours = 1;

            foreach (Joueur joueur in game.Game.Joueurs)
            {
                for (int i = 1; i <= game.Game.Mission.NbTours; i++)
                {
                    joueur.Actions.Add(i, null);
                }
            }
            RegisterGame(game.Game);
            return game.Game.Id;
        }

        /// <summary>
        /// Ajoute les rampes à une partie
        /// </summary>
        private void InitialiserRampes(GameContext game)
        {
            List<Rampe> allRampes = SpaceAlertData.GetAll<Rampe>().Select(kvp => kvp.Value).ToList();
            foreach (Zone zone in Enum.GetValues(typeof(Zone)))
            {
                game.Rampes.Add(zone, allRampes.GetNextRandom(true));
            }
            game.RampeInterne = allRampes.GetNextRandom(true);
        }

        /// <summary>
        /// Récupère toutes les parties en attente de joueurs
        /// </summary>
        /// <returns></returns>
        public List<Game> RecupererGameEnAttente()
        {
            IEnumerable<GameContext> contexts = unitOfWork.Context.GameContext
                .Include(g => g.Game)
                .Include("Game.Joueurs")
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
            return unitOfWork.Context.GameContext.Include(g => g.Game).SingleOrDefault(g => g.GameId == gameId);
        }

        /// <summary>
        /// Récupère la couleur actuelle d'un joueur dans une partie
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="charName"></param>
        /// <returns></returns>
        public string PlayerColor(Guid gameId, string charName)
        {
            // Joueur joueur = SpaceAlertData.Game(gameId).Partie.Joueurs.FirstOrDefault(j => j.Personnage.Nom == charName);
            Joueur joueur = unitOfWork.GameProvider.GetUniqueResult(g => g.Id == gameId).Joueurs.FirstOrDefault(j => j.Personnage.Nom == charName);
            return joueur != null ? joueur.Couleur : null;
        }

        public static string ProchaineCouleur(GameContext game, string charName)
        {
            int index = SpaceAlertData.PlayerColors.IndexOf(game.Game.Joueurs.First(j => j.Personnage.Nom == charName).Couleur);
            int current = index;
            string color;
            do
            {
                current = (current + 1) % SpaceAlertData.PlayerColors.Count;
                color = SpaceAlertData.PlayerColors[current];
            } while (current != index && game.Game.Joueurs.Select(j => j.Couleur).Contains(color));

            game.Game.Joueurs.First(j => j.Personnage.Nom == charName).Couleur = color;

            return color;
        }

        /// <summary>
        /// Set une couleur et la retourne
        /// </summary>
        /// <param name="gameId">la partie concernée</param>
        /// <param name="charName">Le nom du personnage à qui assigner la couleur</param>
        /// <returns></returns>
        public string ProchaineCouleur(Guid gameId, string charName)
        {
            GameContext game = GetGame(gameId);
            return ProchaineCouleur(game, charName);
        }

        /// <summary>
        /// Ajoute un joueur à une partie
        /// </summary>
        /// <param name="gameId">La partie concernée</param>
        /// <param name="memberId">L'id du membre qui rejoint la partie</param>
        /// <param name="charName">Le nom du personnage du membre</param>
        public GameContext AjouterJoueur(Guid gameId, long memberId, string charName)
        {

            GameContext game = GetGame(gameId);

            if (game.Game.Joueurs.Count == game.Game.NbJoueurs)
            {
                throw new PartiePleineException();
            }

            // On vérifie qu'un joueur ne porte pas déjà ce nom
            if (game.Game.Joueurs.Any(j => j.Personnage.Nom == charName))
            {
                throw new NomDejaUtiliseException();
            }

            // On ajoute le joueur à la partie
            game.Game.Joueurs.Add(JoueurFactory.CreateJoueur(unitOfWork.PersonnageProvider.Get(memberId, charName), false, game.Game));

            // On lui assigne une couleur
            ProchaineCouleur(gameId, charName);

            return game;
        }

        /// <summary>
        /// Enregistre une partie lorsqu'elle est lancée
        /// </summary>
        private void RegisterGame(Game game)
        {
            foreach (Joueur joueur in game.Joueurs)
            {
                unitOfWork.Context.Personnages.Attach(joueur.Personnage);
                unitOfWork.Context.Membres.Attach(joueur.Personnage.Membre);
                unitOfWork.JoueurProvider.RegisterGame(joueur, game);
            }
            unitOfWork.GameProvider.Add(game);
        }
    }
}
