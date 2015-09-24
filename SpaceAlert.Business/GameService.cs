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

            res.Game.Joueurs[0].Couleur = ProchaineCouleur(res, res.Game.Joueurs[0].Personnage.Nom);
            InitialiserRampes(res);
            unitOfWork.GameContextProvider.Add(res);

            unitOfWork.Context.SaveChanges();

            return res.Id;
        }

        /// <summary>
        /// Récupère une mission aléatoire d'un type défini
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
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
                game.Rampes.Add(new RampeInZone{
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
            return unitOfWork.Context.GameContext
                .Include(g => g.Game)
                .Include(g => g.Game.Joueurs)
                .SingleOrDefault(g => g.Id == gameId);
        }

        /// <summary>
        /// Récupère la couleur actuelle d'un joueur dans une partie
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="characterName"></param>
        /// <returns></returns>
        public string PlayerColor(Guid gameId, string characterName)
        {
            Joueur joueur = unitOfWork.GameProvider.GetUniqueResult(g => g.Id == gameId).Joueurs.FirstOrDefault(j => j.Personnage.Nom == characterName);
            return joueur != null ? joueur.Couleur : null;
        }

        public string ProchaineCouleur(GameContext game, string characterName)
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
        /// Set une couleur et la retourne
        /// </summary>
        /// <param name="gameId">la partie concernée</param>
        /// <param name="characterName">Le nom du personnage à qui assigner la couleur</param>
        /// <returns></returns>
        public string ProchaineCouleur(Guid gameId, string characterName)
        {
            GameContext game = GetGame(gameId);
            return ProchaineCouleur(game, characterName);
        }

        /// <summary>
        /// Ajoute un joueur à une partie
        /// </summary>
        /// <param name="gameId">La partie concernée</param>
        /// <param name="memberId">L'id du membre qui rejoint la partie</param>
        /// <param name="characterName">Le nom du personnage du membre</param>
        public GameContext AjouterJoueur(Guid gameId, long memberId, string characterName)
        {

            GameContext game = GetGame(gameId);

            if (game.Game.Joueurs.Count == game.Game.NbJoueurs)
            {
                throw new PartiePleineException();
            }

            // On vérifie qu'un joueur ne porte pas déjà ce nom
            if (game.Game.Joueurs.Any(j => j.Personnage.Nom == characterName))
            {
                throw new NomDejaUtiliseException();
            }

            // On ajoute le joueur à la partie
            game.Game.Joueurs.Add(JoueurFactory.CreateJoueur(unitOfWork.PersonnageProvider.Get(memberId, characterName), false, game.Game));

            // On lui assigne une couleur
            ProchaineCouleur(gameId, characterName);

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
