using System;

namespace SpaceAlert.Model.Jeu.Evenements
{
    /// <summary>
    /// Evénement d'annonce de fin d'une phase
    /// </summary>
    public class FinDePartie : Evenement
    {
        /// <summary>
        /// Le temps restant avant la fin de la phase
        /// </summary>
        public TimeSpan TempsRestant { get; set; }

    }
}
