using SpaceAlert.Model.Helpers.Enums;
using SpaceAlert.Model.Plateau;
using System;
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

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
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

        /// <summary>
        /// Gets or sets the menaces externes names.
        /// </summary>
        /// <value>
        /// The menaces externes names.
        /// </value>
        public List<MenaceInZone> MenacesExternesNames { get; set; }

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
        /// Gets or sets the campagne identifier.
        /// </summary>
        /// <value>
        /// The campagne identifier.
        /// </value>
        [ForeignKey("Campagne")]
        public int? CampagneId { get; set; }

        /// <summary>
        /// La campagne à laquelle appartient la partie
        /// </summary>
        public virtual Campagne Campagne { get; set; }

        /// <summary>
        /// La difficulté de la partie
        /// </summary>
        public Couleur Difficulte { get; set; }

        /// <summary>
        /// Le nombre de joueurs
        /// </summary>
        public int NbJoueurs { get; set; }

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
