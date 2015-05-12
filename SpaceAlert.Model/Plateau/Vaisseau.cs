using SpaceAlert.Model.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceAlert.Model.Plateau
{
    public class Vaisseau
    {
        private int NbCapsules { get; set; }

        private int NbRoquettes { get; set; }

        private bool Interceptors { get; set; }

        private List<Salle> Salles { get; set; }

        private IList<bool> RobotsActifs { get; set; }
    }
}
