using SpaceAlert.Model.Helpers.Enums;
using SpaceAlert.Model.Menaces;
using System;

namespace SpaceAlert.Model.Jeu.Evenements
{
    /// <summary>
    /// Décrit un événement correspondant à l'arrivée d'une menace
    /// </summary>
    public class EvenementMenace: Evenement
    {
        /// <summary>
        /// Est-ce un rapport confirmé ?
        /// Si non, la menace n'est pas jouée lorsqu'il y a moins de 5 joueurs
        /// </summary>
        public bool Confirme { get; set; }

        /// <summary>
        /// La zone sur laquelle arrive la menace
        /// </summary>
        public Zone Zone { get; set; }

        /// <summary>
        /// Le tour auquel la menace arrive
        /// </summary>
        public int TourArrive { get; set; }

        /// <summary>
        /// Le type de menace
        /// </summary>
        public TypeMenace Type { get; set; }

        /// <summary>
        /// La menace associée
        /// </summary>
        public string MenaceName { get; set; }

    }
}
