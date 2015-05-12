using SpaceAlert.Model.Helpers;
using System.Collections.Generic;

namespace SpaceAlert.Model.Jeu
{
    /// <summary>
    /// Décrit les événements et le type d'une mission
    /// </summary>
    public class Mission
    {
        /// <summary>
        /// Le nombre de tours que la partie va durer
        /// </summary>
        public int NbTours { get; set; }

        /// <summary>
        /// Le type de mission
        /// </summary>
        public MissionType TypeMission { get; set; }

        /// <summary>
        /// Les événements qui vont survenir durant la mission
        /// </summary>
        public IList<Evenement> Evenements { get; set; }
    }
}
