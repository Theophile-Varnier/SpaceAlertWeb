using SpaceAlert.Model.Helpers.Enums;
using System.Collections.Generic;

namespace SpaceAlert.Model.Menaces
{
    /// <summary>
    /// Décrit les menaces externes
    /// </summary>
    public class MenaceExterne : Menace
    {

        /// <summary>
        /// Indique si la menace est ciblable par les roquettes
        /// </summary>
        public bool RocketTargetable { get; set; }

    }
}
