using System;

namespace SpaceAlert.Model.Jeu.Evenements
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
