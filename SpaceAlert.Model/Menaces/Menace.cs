using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceAlert.Model.Menaces
{
    public class Menace
    {
        public string Name { get; set; }

        public int Shield { get; set; }

        public int CurrentHp { get; set; }

        public int MaxHp { get; set; }

        public int Speed { get; set; }
    }
}
