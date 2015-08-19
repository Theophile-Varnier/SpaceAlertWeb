using SpaceAlert.Model.Helpers;
using SpaceAlert.Model.Helpers.Enums;
using System.Collections.Generic;

namespace SpaceAlert.Model.Menaces
{
    /// <summary>
    /// Représente la liste des menaces pour une couleur
    /// </summary>
    public class ListOfMenaces : Dictionary<TypeMenace, List<Menace>>
    {
        public ListOfMenaces()
        {

        }

        public ListOfMenaces(Dictionary<TypeMenace, List<Menace>> source)
            : base(source)
        {

        }

        /// <summary>
        /// Récupère une menace aléatoire d'un type donné
        /// Et la supprime de la liste
        /// </summary>
        /// <param name="from">Le type de la menace</param>
        /// <returns>Une menace aléatoire du type demandé</returns>
        public Menace GetNextRandom(TypeMenace from)
        {
            return ContainsKey(from) ? this[from].GetNextRandom() : null;
        }

        /// <summary>
        /// Surcharge de l'opérateur +
        /// </summary>
        /// <param name="l1"></param>
        /// <param name="l2"></param>
        /// <returns></returns>
        public static ListOfMenaces operator +(ListOfMenaces l1, ListOfMenaces l2)
        {
            ListOfMenaces res = new ListOfMenaces();

            foreach (TypeMenace type in l1.Keys)
            {
                res[type] = new List<Menace>(l1[type]);
            }

            foreach (TypeMenace type in l2.Keys)
            {
                if (res.ContainsKey(type))
                {
                    res[type].AddRange(l2[type]);
                }
                else
                {
                    res[type] = new List<Menace>(l2[type]);
                }
            }

            return res;
        }
    }
}
