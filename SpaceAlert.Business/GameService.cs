using SpaceAlert.Business.Exceptions;
using SpaceAlert.Business.Factories;
using SpaceAlert.Model.Helpers;
using SpaceAlert.Model.Helpers.Enums;
using SpaceAlert.Model.Jeu;
using SpaceAlert.Model.Menaces;
using SpaceAlert.Model.Plateau;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SpaceAlert.Business
{
    public class GameService
    {

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
            GameContext res = GameFactory.CreateGame(typeMission, nbJoueurs, blanches, jaunes, rouges, captain);
            InitialiserRampes(res);
            SpaceAlertData.AddGame(res);

            return res.Partie.Id;
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
        /// Démarrer une partie
        /// </summary>
        /// <param name="game"></param>
        /// <returns>L'id de la partie qui a été commencée</returns>
        public Guid DemarrerGame(GameContext game)
        {
            game.Partie.Mission = InitialiserMission(game.Partie.TypeMission);
            game.Statut = StatutPartie.JEU;
            game.TourEnCours = 1;

            foreach (Joueur joueur in game.Partie.Joueurs)
            {
                for (int i = 1; i <= game.Partie.Mission.NbTours; i++)
                {
                    joueur.Actions.Add(i, null);
                }
            }
            return game.Partie.Id;
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
            return SpaceAlertData.GameEnAttente().Where(g => g.Joueurs.Count < g.Joueurs.Capacity).ToList();
        }

        /// <summary>
        /// Récupère une game enregistrée
        /// </summary>
        /// <param name="gameId"></param>
        /// <returns></returns>
        public GameContext GetGame(Guid gameId)
        {
            return SpaceAlertData.Game(gameId);
        }

        /// <summary>
        /// Récupère la couleur actuelle d'un joueur dans une partie
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="charName"></param>
        /// <returns></returns>
        public string PlayerColor(Guid gameId, string charName)
        {
            Joueur joueur = SpaceAlertData.Game(gameId).Partie.Joueurs.FirstOrDefault(j => j.NomPersonnage == charName);
            return joueur != null ? joueur.Couleur : null;
        }

        public static string ProchaineCouleur(GameContext game, string charName)
        {
            int index = SpaceAlertData.PlayerColors.IndexOf(game.Partie.Joueurs.First(j => j.NomPersonnage == charName).Couleur);
            int current = index;
            string color;
            do
            {
                current = (current + 1) % SpaceAlertData.PlayerColors.Count;
                color = SpaceAlertData.PlayerColors[current];
            } while (current != index && game.Partie.Joueurs.Select(j => j.Couleur).Contains(color));

            game.Partie.Joueurs.First(j => j.NomPersonnage == charName).Couleur = color;

            return color;
        }

        /// <summary>
        /// Set une couleur et la retourne
        /// </summary>
        /// <param name="gameId">la partie concernée</param>
        /// <param name="charName">Le nom du personnage à qui assigner la couleur</param>
        /// <returns></returns>
        public static string ProchaineCouleur(Guid gameId, string charName)
        {
            GameContext game = SpaceAlertData.Game(gameId);
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

            GameContext game = SpaceAlertData.Game(gameId);

            if (game.Partie.Joueurs.Count == game.Partie.Joueurs.Capacity)
            {
                throw new PartiePleineException();
            }

            // On vérifie qu'un joueur ne porte pas déjà ce nom
            if (game.Partie.Joueurs.Any(j => j.NomPersonnage == charName))
            {
                throw new NomDejaUtiliseException();
            }

            // On ajoute le joueur à la partie
            game.Partie.Joueurs.Add(JoueurFactory.CreateJoueur(memberId, charName, false, game.Partie));

            // On lui assigne une couleur
            ProchaineCouleur(gameId, charName);

            return game;
        }
    }
}
