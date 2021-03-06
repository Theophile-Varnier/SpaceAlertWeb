﻿using SpaceAlert.Model.Helpers.Enums;
using SpaceAlert.Model.Jeu.Evenements;
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
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Le type de mission
        /// </summary>
        public TypeMission TypeMission { get; set; }

        /// <summary>
        /// Les événements qui vont survenir durant la mission
        /// </summary>
        public IList<Evenement> Evenements { get; set; }
    }
}
