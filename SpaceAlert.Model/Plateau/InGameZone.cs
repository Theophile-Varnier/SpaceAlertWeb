﻿using SpaceAlert.Model.Helpers.Enums;
using System.Collections.Generic;

namespace SpaceAlert.Model.Plateau
{
    /// <summary>
    /// Décrit l'état d'une zone du vaisseau en Jeu
    /// </summary>
    public class InGameZone
    {
        /// <summary>
        /// Constructeur par copie
        /// </summary>
        /// <param name="source"></param>
        public InGameZone(InGameZone source)
        {
            Degats = source.Degats;
            RampeIndice = source.RampeIndice;
            Salles = new Dictionary<Pont, Salle>();
            foreach (Pont p in source.Salles.Keys)
            {
                Salles.Add(p, new Salle(source.Salles[p]));
            }
        }
        /// <summary>
        /// Les deux salles de la zone
        /// </summary>
        public Dictionary<Pont, Salle> Salles { get; set; }

        /// <summary>
        /// La rampe associée à cette zone
        /// </summary>
        public int RampeIndice { get; set; }

        /// <summary>
        /// Le nombre de dégâts subis par la zone
        /// </summary>
        public int Degats { get; set; }
    }
}
