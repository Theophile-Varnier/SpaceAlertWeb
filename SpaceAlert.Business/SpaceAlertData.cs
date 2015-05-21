using SpaceAlert.Model.Helpers.Enums;
using SpaceAlert.Model.Jeu;
using Spring.Context;
using Spring.Context.Support;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SpaceAlert.Business
{
    /// <summary>
    /// Contient des informations sur le jeu
    /// </summary>
    public static class SpaceAlertData
    {
        private static IApplicationContext AppContext;

        private static List<GameContext> Games;

        /// <summary>
        /// Initialisation des constantes du jeu
        /// </summary>
        public static void Init()
        {
            AppContext = ContextRegistry.GetContext();
            Games = new List<GameContext>();
        }

        /// <summary>
        /// Récupère une partie en cours
        /// </summary>
        /// <param name="gameId">L'id de la partie à récupérer</param>
        /// <returns></returns>
        public static GameContext Game(Guid gameId)
        {
            return Games.FirstOrDefault(g => g.Partie.Id == gameId);
        }

        /// <summary>
        /// Récupère la liste des parties en attente
        /// </summary>
        /// <returns></returns>
        public static List<Game> GameEnAttente()
        {
            return Games.Where(c => c.Statut == StatutPartie.CREATION).Select(c => c.Partie).ToList();
        }

        /// <summary>
        /// Récupère un objet depuis la configuration
        /// </summary>
        /// <typeparam name="T">Le type de l'objet</typeparam>
        /// <param name="objectName">Le nom de l'objet</param>
        /// <returns></returns>
        public static T GetObject<T>(string objectName)
        {
            return (T) AppContext.GetObject(objectName);
        }

        /// <summary>
        /// Ajoute une parties à celles connues
        /// </summary>
        /// <param name="gameContext">La partie à ajouter</param>
        public static void AddGame(GameContext gameContext)
        {
            Games.Add(gameContext);
        }

        /// <summary>
        /// Récupère tous les objets configurés du type T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Dictionary<string, T> GetAll<T>()
        {
            return AppContext.GetObjects<T>().ToDictionary(e => e.Key, e => e.Value);
        }

    }
}