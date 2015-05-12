using SpaceAlert.Model.Helpers;

namespace SpaceAlert.Model.Plateau
{
    public class Salle
    {
        public CAction ActionC { get; set; }

        public int EnergieMax { get; set; }

        public int EnergieCourante { get; set; }

        public int CanonPower { get; set; }

        public int CanonRange { get; set; }
    }
}
