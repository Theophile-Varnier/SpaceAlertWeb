using System;
using System.Data.Entity;
using System.Linq;

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
        protected T GetWith(Func<T, bool> filtres)
        {
            return Table.SingleOrDefault(filtres);
        }
    }
}
