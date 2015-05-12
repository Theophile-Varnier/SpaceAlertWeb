using System.Collections;
using System.Collections.Generic;
using SpaceAlert.Model.Helpers;

namespace SpaceAlert.Model.Plateau
{
    public class InGameZone
    {
        public Dictionary<Pont, Salle> Salles { get; set; }

        public int RampeIndice { get; set; }

        public int Degats { get; set; }
    }
}
