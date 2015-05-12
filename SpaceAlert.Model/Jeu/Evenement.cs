using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceAlert.Model.Jeu
{
    /// <summary>
    /// Décrit les différents événements pouvant survenir
    /// </summary>
    public abstract class Evenement
    {
        /// <summary>
        /// Le moment auquel est annoncé l'événement
        /// </summary>
        public TimeSpan Annonce { get; set; }

        /// <summary>
        /// Résout l'événement
        /// </summary>
        public abstract void Resolve();
    }
}
