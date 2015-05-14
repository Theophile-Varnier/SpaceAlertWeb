using SpaceAlert.Model.Helpers.Enums;
using SpaceAlert.Model.Menaces;
using SpaceAlert.Model.Plateau;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceAlert.Model.Helpers
{
    public static class MenaceActions
    {
        public static void Attack(Menace source, Vaisseau target, Zone from)
        {
            target.NbCapsules = 0;
        }
    }
}
