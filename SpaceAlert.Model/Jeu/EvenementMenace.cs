using SpaceAlert.Model.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceAlert.Model.Jeu
{
    /// <summary>
    /// Décrit un événement correspondant à l'arrivée d'une menace
    /// </summary>
    public class EvenementMenace: Evenement
    {
        public bool Confirme { get; set; }

        public Zone Zone { get; set; }

        public int TourArrive { get; set; }

        public override void Resolve()
        {
            throw new NotImplementedException();
        }
    }
}
