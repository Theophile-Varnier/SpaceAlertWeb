using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceAlert.Model.Menaces
{
    /// <summary>
    /// Représente la liste des menaces pour une couleur
    /// </summary>
    public class ListOfMenaces
    {
        /// <summary>
        /// Les menaces externes normales
        /// </summary>
        public List<Menace> ExternesNormales { get; set; }

        /// <summary>
        /// Les menaces externes sérieuses
        /// </summary>
        public List<Menace> ExternesSerieuses { get; set; }

        /// <summary>
        /// Les menaces internes normales
        /// </summary>
        public List<Menace> InternesNormales { get; set; }

        /// <summary>
        /// Les menaces internes sérieuses
        /// </summary>
        public List<Menace> InternesSerieuses { get; set; }
    }
}
