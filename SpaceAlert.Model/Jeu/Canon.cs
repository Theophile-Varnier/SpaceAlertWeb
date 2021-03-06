﻿
namespace SpaceAlert.Model.Jeu
{
    /// <summary>
    /// Représente les différents types de canons
    /// </summary>
    public class Canon
    {
        /// <summary>
        /// La portée du canon
        /// </summary>
        public int Range { get; set; }

        /// <summary>
        /// La puissance du canon
        /// </summary>
        public int Power { get; set; }

        /// <summary>
        /// Spécifie si le canon a déjà tiré ce tour-ci
        /// </summary>
        public bool HasShot { get; set; }

        /// <summary>
        /// Si le canon consomme de l'énergie ou non
        /// </summary>
        public bool ConsumeEnergy;

        /// <summary>
        /// Si le canon attaque sur toutes les zones
        /// </summary>
        public bool Spray;
    }
}
