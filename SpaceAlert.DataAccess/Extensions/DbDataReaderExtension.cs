using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceAlert.DataAccess.Extensions
{
    public static class DbDataReaderExtension
    {
        /// <summary>
        /// Récupère un entier à partir du nom de sa colonne
        /// </summary>
        /// <param name="reader">Le reader contenant la valeur recherchée</param>
        /// <param name="paramName">Le nom de la colonne</param>
        /// <returns></returns>
        public static int GetInt32(this DbDataReader reader, string paramName)
        {
            return reader.Get(paramName, reader.GetInt32, 0);
        }

        /// <summary>
        /// Récupère un long à partir du nom de sa colonne
        /// </summary>
        /// <param name="reader">Le reader contenant la valeur recherchée</param>
        /// <param name="paramName">Le nom de la colonne</param>
        /// <returns></returns>
        public static long GetInt64(this DbDataReader reader, string paramName)
        {
            return reader.Get(paramName, reader.GetInt64, 0);
        }

        /// <summary>
        /// Récupère une chaîne à partir du nom de sa colonne
        /// </summary>
        /// <param name="reader">Le reader contenant la valeur recherchée</param>
        /// <param name="paramName">Le nom de la colonne</param>
        /// <returns></returns>
        public static string GetString(this DbDataReader reader, string paramName)
        {
            return reader.Get(paramName, reader.GetString, string.Empty);
        }

        /// <summary>
        /// Récupère un double à partir du nom de sa colonne
        /// </summary>
        /// <param name="reader">Le reader contenant la valeur recherchée</param>
        /// <param name="paramName">Le nom de la colonne</param>
        /// <returns></returns>
        public static double GetDouble(this DbDataReader reader, string paramName)
        {
            return reader.Get(paramName, reader.GetDouble, 0);
        }

        /// <summary>
        /// Récupère un booléen à partir du nom de sa colonne
        /// </summary>
        /// <param name="reader">Le reader contenant la valeur recherchée</param>
        /// <param name="paramName">Le nom de la colonne</param>
        /// <returns></returns>
        public static bool GetBoolean(this DbDataReader reader, string paramName)
        {
            return reader.Get(paramName, reader.GetBoolean, false);
        }

        /// <summary>
        /// Méthode générique de récupération de champs dans un Reader
        /// </summary>
        /// <typeparam name="T">Le type de la valeur recherchée</typeparam>
        /// <param name="reader">Le reader duquel on extrait la valeur</param>
        /// <param name="paramName">Le nom de la colonne recherchée</param>
        /// <param name="returnFunction">La fonction servant à récupérer le champ</param>
        /// <param name="defaultValue">La valeur renvoyée par défaut</param>
        /// <returns>Le champ recherché ou une valeur par défaut</returns>
        private static T Get<T>(this DbDataReader reader, string paramName, Func<int, T> returnFunction, T defaultValue)
        {
            int i = 0;
            while (i < reader.FieldCount)
            {
                if (reader.GetName(i) == paramName && !reader.IsDBNull(i))
                {
                    return returnFunction(i);
                }
                i++;
            }
            return defaultValue;
        }
    }
}
