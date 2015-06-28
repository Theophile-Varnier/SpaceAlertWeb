using SpaceAlert.Business;
using SpaceAlert.Model.Helpers;
using SpaceAlert.Model.Helpers.Enums;
using SpaceAlert.Model.Jeu;
using SpaceAlert.Model.Menaces;
using SpaceAlert.Model.Plateau;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SpaceAlert.Services
{
    public class GameService
    {
        /// <summary>
        /// Initialise une nouvelle partie
        /// </summary>
        /// <param name="game"></param>
        /// <returns></returns>
        public GameContext InitialiserGame(Game game)
        {
            // Initialisation du contexte
            GameContext res = new GameContext
            {
                Statut = StatutPartie.CREATION,
                Partie = game,
                MenacesDisponibles = new ListOfMenaces()
            };

            // Ajout des menaces pouvant apparaître
            if (res.Partie.Difficulte.HasFlag(Couleur.BLANCHE))
            {
                res.MenacesDisponibles += SpaceAlertData.GetObject<ListOfMenaces>("MenacesBlanches");
            }
            //if (res.Partie.Difficulte.HasFlag(Couleur.JAUNE))
            //{
            //    res.MenacesDisponibles += SpaceAlertData.GetObject<ListOfMenaces>("MenacesJaunes");
            //}
            //if (res.Partie.Difficulte.HasFlag(Couleur.ROUGE))
            //{
            //    res.MenacesDisponibles += SpaceAlertData.GetObject<ListOfMenaces>("MenacesRouges");
            //}

            // Initialisation de l'état du vaisseau
            res.Partie.Vaisseau = SpaceAlertData.GetObject<Vaisseau>("Vaisseau");

            // Ajout de la partie à l'ensemble des parties connues
            SpaceAlertData.AddGame(res);

            return res;
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
        public Guid InitialiserGame(TypeMission typeMission, int nbJoueurs, bool blanches, bool jaunes, bool rouges, List<string> playerNames)
        {
            // Créé la partie
            Game game = new Game
            {
                TypeMission = typeMission,
                DateCreation = DateTime.Now,
                Joueurs = new List<Joueur>(nbJoueurs)
            };

            // Ajoute les joueurs
            foreach (string playerName in playerNames)
            {
                game.Joueurs.Add(new Joueur { NomPersonnage = playerName });
            }

            // Initialise le contexte
            GameContext res = new GameContext
            {
                Statut = StatutPartie.CREATION,
                Partie = game,
                MenacesDisponibles = new ListOfMenaces()
            };

            // Ajout des menaces
            if (blanches)
            {
                game.Difficulte |= Couleur.BLANCHE;
                res.MenacesDisponibles += SpaceAlertData.GetObject<ListOfMenaces>("MenacesBlanches");
            }
            if (jaunes)
            {
                game.Difficulte |= Couleur.JAUNE;
                //res.MenacesDisponibles += SpaceAlertData.GetObject<ListOfMenaces>("MenacesJaunes");
            }
            if (rouges)
            {
                game.Difficulte |= Couleur.ROUGE;
                //res.MenacesDisponibles += SpaceAlertData.GetObject<ListOfMenaces>("MenacesRouges");
            }

            SpaceAlertData.AddGame(res);

            // Initialise les couleurs des joueurs
            foreach (Joueur joueur in game.Joueurs)
            {
                ProchaineCouleur(game.Id, joueur.NomPersonnage);
            }

            return game.Id;
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
            return game.Partie.Id;
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

        /// <summary>
        /// Set une couleur et la retourne
        /// </summary>
        /// <param name="gameId">la partie concernée</param>
        /// <param name="charName">Le nom du personnage à qui assigner la couleur</param>
        /// <returns></returns>
        public string ProchaineCouleur(Guid gameId, string charName)
        {
            GameContext game = SpaceAlertData.Game(gameId);
            int index = SpaceAlertData.PlayerColors.IndexOf(game.Partie.Joueurs.First(j => j.NomPersonnage == charName).Couleur);
            int current = index;
            string color;
            do
            {
                current++;
                color = SpaceAlertData.PlayerColors[current % SpaceAlertData.PlayerColors.Count];
            } while (current != index && game.Partie.Joueurs.Select(j => j.Couleur).Contains(color));

            game.Partie.Joueurs.First(j => j.NomPersonnage == charName).Couleur = color;

            return color;
        }
    }
}
