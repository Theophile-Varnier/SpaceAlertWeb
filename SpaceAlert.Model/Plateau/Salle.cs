﻿using SpaceAlert.Model.Helpers.Enums;

namespace SpaceAlert.Model.Plateau
{
    /// <summary>
    /// Décrit l'état d'une salle en cours de partie
    /// </summary>
    public class Salle
    {
        /// <summary>
        /// Constructeur par copie
        /// </summary>
        /// <param name="source"></param>
        public Salle(Salle source)
        {
            Zone = source.Zone;
            Pont = source.Pont;
            ActionC = source.ActionC;
            EnergieMax = source.EnergieMax;
            EnergieCourante = source.EnergieCourante;
            CanonPower = source.CanonPower;
            CanonRange = source.CanonRange;
        }

        /// <summary>
        /// La zone à laquelle la salle appartient
        /// </summary>
        public Zone Zone { get; set; }

        /// <summary>
        /// Le pont auquel la salle appartient
        /// </summary>
        public Pont Pont { get; set; }

        /// <summary>
        /// Le type d'action que génère le bouton C
        /// </summary>
        public CAction ActionC { get; set; }

        /// <summary>
        /// Le nombre max d'énergie (resource ou bouclier)
        /// </summary>
        public int EnergieMax { get; set; }

        /// <summary>
        /// Le nombre courant d'énergie (resource ou bouclier)
        /// </summary>
        public int EnergieCourante { get; set; }

        /// <summary>
        /// La puissance du canon de la salle
        /// </summary>
        public int CanonPower { get; set; }

        /// <summary>
        /// La portée du canon de la salle
        /// </summary>
        public int CanonRange { get; set; }
    }
}
