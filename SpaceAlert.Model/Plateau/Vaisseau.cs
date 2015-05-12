using System.Collections.Generic;
using System.Linq;
using SpaceAlert.Model.Helpers;

namespace SpaceAlert.Model.Plateau
{
    public class Vaisseau
    {
        private int NbCapsules { get; set; }

        private int NbRoquettes { get; set; }

        private bool Interceptors { get; set; }

        private Dictionary<Zone, InGameZone> Zones { get; set; }

        private IList<bool> RobotsActifs { get; set; }

        public Salle Salle(Zone z, Pont p)
        {
            return Zones[z].Salles[p];
        }
    }
}
