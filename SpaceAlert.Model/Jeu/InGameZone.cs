using System.Collections.Generic;
using SpaceAlert.Model.Helpers.Enums;
using SpaceAlert.Model.Plateau;

namespace SpaceAlert.Model.Jeu
{
    /// <summary>
    /// Décrit l'état d'une zone du vaisseau en Jeu
    /// </summary>
    public class InGameZone
    {

        /// <summary>
        /// Les deux salles de la zone
        /// </summary>
        public Dictionary<Pont, Salle> Salles { get; set; }

        /// <summary>
        /// La rampe associée à cette zone
        /// </summary>
        public int RampeIndice { get; set; }

        /// <summary>
        /// Le nombre de dégâts subis par la zone
        /// </summary>
        public int Degats { get; set; }
    }
}
