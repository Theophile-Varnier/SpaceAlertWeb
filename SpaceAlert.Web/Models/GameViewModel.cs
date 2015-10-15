using SpaceAlert.Model.Helpers.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SpaceAlert.Web.Models
{
    public class GameViewModel : AbstractViewModel
    {
        /// <summary>
        /// Gets or sets the type mission.
        /// </summary>
        /// <value>
        /// The type mission.
        /// </value>
        public TypeMission TypeMission { get; set; }

        /// <summary>
        /// Gets or sets the game identifier.
        /// </summary>
        /// <value>
        /// The game identifier.
        /// </value>
        public int GameId { get; set; }

        /// <summary>
        /// Gets or sets the date creation.
        /// </summary>
        /// <value>
        /// The date creation.
        /// </value>
        public DateTime DateCreation { get; set; }

        /// <summary>
        /// Gets or sets the nb joueurs.
        /// </summary>
        /// <value>
        /// The nb joueurs.
        /// </value>
        public int NbJoueurs { get; set; }

        /// <summary>
        /// Gets or sets the nb androids.
        /// </summary>
        /// <value>
        /// The nb androids.
        /// </value>
        public int NbAndroids { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="GameViewModel"/> is blanches.
        /// </summary>
        /// <value>
        ///   <c>true</c> if blanches; otherwise, <c>false</c>.
        /// </value>
        [Display(Name = "Cartes Blanches")]
        public bool Blanches { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="GameViewModel"/> is jaunes.
        /// </summary>
        /// <value>
        ///   <c>true</c> if jaunes; otherwise, <c>false</c>.
        /// </value>
        [Display(Name = "Cartes Jaunes")]
        public bool Jaunes { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="GameViewModel"/> is rouges.
        /// </summary>
        /// <value>
        ///   <c>true</c> if rouges; otherwise, <c>false</c>.
        /// </value>
        [Display(Name = "Cartes Rouges")]
        public bool Rouges { get; set; }

        /// <summary>
        /// Gets or sets the players.
        /// </summary>
        /// <value>
        /// The players.
        /// </value>
        public List<PlayerViewModel> Players { get; set; }

        /// <summary>
        /// Vérifie que le modèle est valide
        /// </summary>
        /// <returns></returns>
        public override bool Validate()
        {
            if (ErrorMessages != null)
            {
                ErrorMessages.Clear();
            }
            else
            {
                ErrorMessages = new List<string>();
            }

            if (!Blanches && !Jaunes && !Rouges)
            {
                ErrorMessages.Add("La mission doit contenir au moins un type de menaces");
                return false;
            }
            if (NbJoueurs < 0 || NbJoueurs > 5)
            {
                ErrorMessages.Add("Problème de configuration");
                return false;
            }
            if (NbAndroids < 0 || NbAndroids > 4)
            {
                ErrorMessages.Add("Problème de configuration");
                return false;
            }
            return NbAndroids + NbJoueurs <= 5;
        }
    }
}