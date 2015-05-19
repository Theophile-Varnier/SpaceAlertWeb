using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Linq;
using System.Text.RegularExpressions;

namespace SpaceAlert.DataAccess.Extensions
{
    public class Command: IDisposable
    {
        private DbCommand internCommand;

        /// <summary>
        /// La connexion à utiliser
        /// </summary>
        public DbConnection Connection
        {
            get
            {
                return internCommand.Connection;
            }
            set
            {
                internCommand.Connection = value;
            }
        }

        /// <summary>
        /// La transaction de la commande
        /// </summary>
        public DbTransaction Transaction
        {
            get
            {
                return internCommand.Transaction;
            }
            set
            {
                internCommand.Transaction = value;
            }
        }

        /// <summary>
        /// Wrapper de la méthode Prepare
        /// </summary>
        public void Prepare()
        {
            internCommand.Prepare();
        }

        private Dictionary<string, object> parameters = new Dictionary<string, object>();

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        public Command()
        {
            internCommand = new MySqlCommand();
        }

        /// <summary>
        /// Constructeur 
        /// </summary>
        /// <param name="cmdText">La commande à exécuter</param>
        public Command(string cmdText)
        {
            internCommand = new MySqlCommand(cmdText);
        }

        /// <summary>
        /// Wrapper de l'insertion
        /// </summary>
        public void ExecuteNonQuery()
        {
            AddParameters();
            internCommand.ExecuteNonQuery();
        }

        /// <summary>
        /// Wrapper de la lecture
        /// </summary>
        /// <returns></returns>
        public DbDataReader ExecuteReader()
        {
            AddParameters();
            return internCommand.ExecuteReader();
        }

        /// <summary>
        /// Ajout des paramètres
        /// (dans le bon ordre)
        /// </summary>
        private void AddParameters()
        {
            Regex regex = new Regex("@[A-Z|a-z|0-9]+");
            MatchCollection matches = regex.Matches(internCommand.CommandText);
            foreach (Match match in matches.Cast<Match>().OrderBy(m => m.Index))
            {
                internCommand.Parameters.Add(new MySqlParameter(match.Value, parameters[match.Value]));

            }
        }

        /// <summary>
        /// Ajout d'un paramètre dans le dictionaire
        /// </summary>
        /// <param name="paramName">le nom du paramètre</param>
        /// <param name="value">la valeur à ajouter</param>
        public void AddWithValue(string paramName, object value)
        {
            parameters.Add(paramName, value);
        }

        /// <summary>
        /// Wrapper du dispose
        /// </summary>
        public void Dispose()
        {
            internCommand.Dispose();
        }

        /// <summary>
        /// Statique méthode contenant les informations sur le SGBD
        /// </summary>
        /// <returns></returns>
        public static DbConnection GetConnexion()
        {
            return new MySqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        }
    }
}
