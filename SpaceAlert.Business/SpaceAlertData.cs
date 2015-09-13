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

        public static readonly List<string> PlayerColors = new List<string>
        {
            "blue",
            "red",
            "green",
            "yellow",
            "purple"
        };

        public static readonly List<int> DebutPhases = new List<int>
        {
            0,
            3,
            7
        };

        public static readonly int RocketDamages = 3;

        /// <summary>
        /// Initialisation des constantes du jeu
        /// </summary>
        public static void Init()
        {
            AppContext = ContextRegistry.GetContext();
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