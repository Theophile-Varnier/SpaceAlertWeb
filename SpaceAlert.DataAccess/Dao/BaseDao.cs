using SpaceAlert.DataAccess.Extensions;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace SpaceAlert.DataAccess.Dao
{
    /// <summary>
    /// Dao de base
    /// </summary>
    /// <typeparam name="T">Type de données à insérer</typeparam>
    public abstract class BaseDao<T> where T : class
    {

        /// <summary>
        /// Requête ne retournant pas de résultat
        /// </summary>
        /// <param name="cmdText">la commande à exécuter</param>
        /// <param name="tran">La transaction à utiliser</param>
        /// <param name="dto">L'objet à partir duquel on ajoute les paramètres</param>
        /// <param name="addParams">La méthode servant à ajouter les paramètres</param>
        /// <param name="conn">La connection à utiliser</param>
        protected void ExecuteNonQuery(string cmdText, DbConnection conn, object dto = null, Action<Command, object> addParams = null, DbTransaction tran = null)
        {
            try
            {
                // Préparation de la requête
                using (Command cmd = new Command(cmdText))
                {
                    cmd.Connection = conn;

                    if (tran != null)
                    {
                        cmd.Transaction = tran;
                    }

                    cmd.Prepare();

                    // Ajout des paramètres
                    if (addParams != null)
                    {
                        addParams(cmd, dto);
                    }

                    // Exécution
                    cmd.ExecuteNonQuery();
                }
            }
            catch (DbException e)
            {
                switch (e.ErrorCode)
                {
                        // La donnée existe déjà, tant pis...
                    case 1062:
                        break;
                    default:
                        throw new DaoException("Erreur dans l'insertion en BD : ", e);
                }
            }
        }

        /// <summary>
        /// Récupère des données en base
        /// </summary>
        /// <param name="cmdText">la commande à exécuter</param>
        /// <param name="conn">La connexion à utiliser</param>
        /// <param name="dto">L'objet à partir duquel on ajoute les paramètres</param>
        /// <param name="addParams">La méthode servant à ajouter les paramètres</param>
        /// <returns></returns>
        protected List<T> ExecuteReader(string cmdText, DbConnection conn, object dto = null, Action<Command, object> addParams = null, DbTransaction tran = null)
        {
            List<T> res = new List<T>();
            try
            {
                // Préparation de la requête
                // conn.Open();
                using (Command cmd = new Command(cmdText))
                {
                    cmd.Connection = conn;

                    if (tran != null)
                    {
                        cmd.Transaction = tran;
                    }

                    cmd.Prepare();

                    // Ajout des paramètres
                    if (addParams != null && dto != null)
                    {
                        addParams(cmd, dto);
                    }

                    // Exécution
                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                res.Add(RecordToDto(reader));
                            }
                        }
                    }

                    return res;
                }
            }
            catch (DbException e)
            {
                throw new DaoException("Erreur dans l'insertion en BD", e);
            }
        }

        /// <summary>
        /// Bind un reader en objet
        /// </summary>
        /// <param name="reader">Le reader duquel tirer les infos</param>
        /// <returns></returns>
        public abstract T RecordToDto(DbDataReader reader);
    }
}
