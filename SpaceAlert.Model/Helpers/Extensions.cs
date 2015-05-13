using System;

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
    }
}
