using System;
using System.Collections.Generic;
using SpaceAlert.Model.Helpers.Enums;
using SpaceAlert.Model.Plateau;

namespace SpaceAlert.Model.Jeu
{
    /// <summary>
    /// Représente une partie en cours
    /// </summary>
    public class Game
    {
        /// <summary>
        /// Constructeur par défaut
        /// Initialise les variables non dépendantes du contexte
        /// </summary>
        public Game()
        {
            Id = Guid.NewGuid();
            Joueurs = new List<Joueur>();
        }

        public Guid Id { get; set; }

        public Vaisseau Vaisseau { get; set; }

        /// <summary>
        /// Le type de mission
        /// </summary>
        public TypeMission TypeMission { get; set; }

        public Mission Mission { get; set; }

        public Couleur Difficulte { get; set; }

        public List<Joueur> Joueurs { get; set; }

        public DateTime DateCreation { get; set; }
    }
}
