using SpaceAlert.Model.Helpers.Enums;
using SpaceAlert.Model.Plateau;
using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaceAlert.Model.Jeu
{
    /// <summary>
    /// Représente une partie en cours
    /// </summary>
    [Table("Games")]
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

        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Le vaisseau
        /// </summary>
        [NotMapped]
        public Vaisseau Vaisseau { get; set; }

        /// <summary>
        /// Les menaces externes en cours de résolution
        /// </summary>
        [NotMapped]
        public Dictionary<Zone, List<InGameMenace>> MenacesExternes { get; set; }

        public List<string> MenacesExternesNames
        {
            get
            {
                return MenacesExternes.SelectMany(m => m.Value.Select(me => me.Menace.Name)).ToList();
            }
        }

        /// <summary>
        /// Le type de mission
        /// </summary>
        public TypeMission TypeMission { get; set; }

        /// <summary>
        /// La mission
        /// </summary>
        [NotMapped]
        public Mission Mission { get; set; }

        /// <summary>
        /// La difficulté de la partie
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

        /// <summary>
        /// La date de fin de la partie
        /// </summary>
        public DateTime DateFin { get; set; }
    }
}
