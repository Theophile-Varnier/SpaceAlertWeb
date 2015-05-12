using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceAlert.Model.Menaces
{
    public class MenaceExterne : Menace
    {
        public bool Targetable { get; set; }

        public bool RocketTargetable { get; set; }
    }
}
