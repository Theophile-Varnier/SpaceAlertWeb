using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceAlert.Model.Jeu
{
    public class FinDePhase : Evenement
    {
        public TimeSpan TempsRestant { get; set; }

        public override void Resolve()
        {
            throw new NotImplementedException();
        }
    }
}
