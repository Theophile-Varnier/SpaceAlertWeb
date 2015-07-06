using SpaceAlert.Model.Helpers.Enums;
using SpaceAlert.Model.Plateau;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceAlert.Model.Menaces
{
    public class Dysfonctionnement : MenaceInterne
    {
        public TypeAction Target { get; set; }

        public List<Salle> Salles { get; set; }
    }
}
