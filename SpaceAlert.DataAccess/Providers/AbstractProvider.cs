using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;

namespace SpaceAlert.DataAccess.Providers
{
    /// <summary>
    /// Classe abstraite modélisant les Providers (~ DAO)
    /// </summary>
    public abstract class AbstractProvider<T> where T : class
    {
        protected readonly SpaceAlertContext context;

        protected DbSet<T> Table;

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="context">Le contexte</param>
        public AbstractProvider(SpaceAlertContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Ajoute une entité à la base
        /// </summary>
        public void Add(T entity)
        {
            Table.Add(entity);
            context.SaveChanges();
        }

        /// <summary>
        /// Supprime un élément de la base
        /// </summary>
        public void Remove(T entity)
        {
            Table.Remove(entity);
            context.SaveChanges();
        }

        /// <summary>
        /// Récupère un unique élément en fonction de ses attributs
        /// </summary>
        public T GetUniqueResult(Func<T, bool> filtres, params Expression<Func<T, object>>[] dataLoadOptions)
        {
            IQueryable<T> res = Table;
            foreach (Expression<Func<T, object>> field in dataLoadOptions)
            {
                res = res.Include(field);
            }
            return res.SingleOrDefault(filtres);
        }

        /// <summary>
        /// Récupère plusieurs éléments en fonction de leurs attributs
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> GetWith(Func<T, bool> filtres)
        {
            return Table.Where(filtres);
        }
    }
}
