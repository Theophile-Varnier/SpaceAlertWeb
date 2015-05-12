using SpaceAlert.Model.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceAlert.Model.Plateau
{
    public class Salle
    {
        public InGameZone Zone { get; set; }

        public Pont Pont { get; set; }

        public CAction ActionC { get; set; }

        public int EnergieMax { get; set; }

        public int EnergieCourante { get; set; }

        public int CanonPower { get; set; }

        public int CanonRange { get; set; }
    }
}
