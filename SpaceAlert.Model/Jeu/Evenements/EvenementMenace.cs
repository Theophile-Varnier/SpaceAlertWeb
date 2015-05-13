using System;
using SpaceAlert.Model.Helpers;

namespace SpaceAlert.Model.Jeu.Evenements
{
    /// <summary>
    /// Décrit un événement correspondant à l'arrivée d'une menace
    /// </summary>
    public class EvenementMenace: Evenement
    {
        public bool Confirme { get; set; }

        public Zone Zone { get; set; }

        public int TourArrive { get; set; }

        public TypeMenace Type { get; set; }

        public override void Resolve()
        {
            throw new NotImplementedException();
        }
    }
}
