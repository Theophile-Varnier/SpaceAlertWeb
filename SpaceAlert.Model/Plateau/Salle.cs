using SpaceAlert.Model.Helpers.Enums;
using SpaceAlert.Model.Jeu;
using System;
using System.Collections.Generic;

namespace SpaceAlert.Model.Plateau
{
    /// <summary>
    /// Décrit l'état d'une salle en cours de partie
    /// </summary>
    public class Salle
    {
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
        /// Le canon de la salle (pas le plus beau hein, le vrai canon)
        /// </summary>
        public Canon Canon { get; set; }

        /// <summary>
        /// Les actions disponibles dans la salle
        /// </summary>
        public Dictionary<TypeAction, Action<Salle, Game>> Actions { get; set; }
    }
}
