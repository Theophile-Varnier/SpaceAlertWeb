using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceAlert.Model.Site
{
    public class Membre
    {
        public long Id { get; set; }

        public string Pseudo { get; set; }

        public string Email { get; set; }

        public string MotDePasse { get; set; }
    }
}
