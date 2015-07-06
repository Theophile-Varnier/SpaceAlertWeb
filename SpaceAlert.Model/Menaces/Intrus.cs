using SpaceAlert.Model.Plateau;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceAlert.Model.Menaces
{
    public class Intrus : MenaceInterne
    {
        /// <summary>
        /// Indique si la menace riposte à une attaque de robot
        /// </summary>
        public bool Riposte { get; set; }

        /// <summary>
        /// La salle dans laquelle l'intrus arrive
        /// </summary>
        public Salle Arrivee { get; set; }
    }
}
