using System;
using System.Collections.Generic;
using System.Linq;

namespace SpaceAlert.Model.Helpers
{
    public static class Extensions
    {
        /// <summary>
        /// Indique si une énum "composée" contient une valeur
        /// </summary>
        /// <typeparam name="T">Le type d'énum en question</typeparam>
        /// <param name="type">L'énum composée</param>
        /// <param name="value">La valeur contenue</param>
        /// <returns>True si "value" est contenue, False sinon</returns>
        public static bool Has<T>(this Enum type, T value)
        {
            try
            {
                return (((int) (object) type & (int) (object) value) == (int) (object) value);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Indique si une énum "composée" est égale à une valeur
        /// </summary>
        /// <typeparam name="T">Le type d'énum en question</typeparam>
        /// <param name="type">L'énum composée</param>
        /// <param name="value">La valeur à comparer</param>
        /// <returns>True si type = value au sens énum, False sinon</returns>
        public static bool Is<T>(this Enum type, T value)
        {
            try
            {
                return (int) (object) type == (int) (object) value;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Récupère un élément aléatoire dans une liste
        /// </summary>
        /// <typeparam name="T">Le type d'éléments de la liste</typeparam>
        /// <param name="list">La liste d'où récupérer l'élément</param>
        /// <param name="remove">Indique si l'élément doit être supprimé ou non</param>
        /// <returns>Un élément aléatoire</returns>
        public static T GetNextRandom<T>(this List<T> list, bool remove = true)
        {
            Random rand = new Random();
            int index = rand.Next() % list.Count;
            T res = list[index];
            if (remove)
            {
                list.RemoveAt(index);
            }
            return res;
        }

        /// <summary>
        /// Récupère un élément aléatoire d'une collection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <returns></returns>
        public static T GetNextRandom<T>(this IEnumerable<T> enumerable)
        {
            Random rand = new Random();
            IEnumerable<T> enumerable1 = enumerable as IList<T> ?? enumerable.ToList();
            int index = rand.Next() % enumerable1.Count();
            return enumerable1.ElementAt(index);
        }

        /// <summary>
        /// Trouve l'indice du premier élément respectant une condition
        /// </summary>
        /// <typeparam name="T">Le type de container</typeparam>
        /// <param name="enumerable">L'énumérable en question</param>
        /// <param name="condition">La condition à respecter</param>
        /// <returns>L'indice du premier élément respectant "condition" s'il existe, -1 sinon</returns>
        public static int FirstIndexOf<T>(this IEnumerable<T> enumerable, Func<T, bool> condition)
        {
            for (int i = 0; i < enumerable.Count(); i++)
            {
                if (condition(enumerable.ElementAt(i)))
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
