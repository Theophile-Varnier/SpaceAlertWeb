using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceAlert.Model.Jeu
{
    /// <summary>
    /// Représente une rampe
    /// </summary>
    public class Rampe
    {
        public int NbCases { get; set; }

        public int IndiceX { get; set; }

        public List<int> IndicesY { get; set; }

        public int IndiceZ { get; set; }
    }
}
