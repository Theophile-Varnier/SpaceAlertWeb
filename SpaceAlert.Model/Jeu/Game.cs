﻿using SpaceAlert.Model.Helpers.Enums;
using SpaceAlert.Model.Plateau;
using System;
using System.Collections.Generic;

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

        /// <summary>
        /// Le vaisseau
        /// </summary>
        public Vaisseau Vaisseau { get; set; }

        /// <summary>
        /// Les menaces externes en cours de résolution
        /// </summary>
        public Dictionary<Zone, List<InGameMenace>> MenacesExternes { get; set; }

        /// <summary>
        /// Le type de mission
        /// </summary>
        public TypeMission TypeMission { get; set; }

        /// <summary>
        /// La mission
        /// </summary>
        public Mission Mission { get; set; }

        /// <summary>
        /// La difficulté dela partie
        /// </summary>
        public Couleur Difficulte { get; set; }

        /// <summary>
        /// Les joueurs
        /// </summary>
        public List<Joueur> Joueurs { get; set; }

        /// <summary>
        /// La date de création de la partie
        /// </summary>
        public DateTime DateCreation { get; set; }
    }
}
