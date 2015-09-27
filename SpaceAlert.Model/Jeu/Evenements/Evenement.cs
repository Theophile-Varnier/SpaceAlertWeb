using System;

namespace SpaceAlert.Model.Jeu.Evenements
{
    /// <summary>
    /// Décrit les différents événements pouvant survenir
    /// </summary>
    public abstract class Evenement
    {
        /// <summary>
        /// Le moment auquel est annoncé l'événement
        /// </summary>
        public TimeSpan Annonce { get; set; }
    }
}
