using SpaceAlert.Model.Helpers.Enums;
using System.Collections.Generic;

namespace SpaceAlert.Model.Jeu
{
    /// <summary>
    /// Représente une rampe
    /// </summary>
    public class Rampe
    {
        public int NbCases { get; set; }

        public Dictionary<TypeCase, List<int>> SpecialCases { get; set; }
    }
}
