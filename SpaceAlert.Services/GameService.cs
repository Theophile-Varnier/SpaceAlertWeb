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
        /// Récupère une partie en cours
        /// </summary>
        /// <param name="gameId">L'id de la partie</param>
        /// <returns></returns>
        public List<Game> RecupererGameEnCours()
        {
            return SpaceAlertData.GameEnAttente();
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
