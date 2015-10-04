using SpaceAlert.Model.Helpers.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SpaceAlert.Web.Models
{
    public class GameViewModel : AbstractViewModel
    {
        public TypeMission TypeMission { get; set; }

        public int GameId { get; set; }

        public DateTime DateCreation { get; set; }

        public int NbJoueurs { get; set; }

        public int NbAndroids { get; set; }

        [Display(Name = "Cartes Blanches")]
        public bool Blanches { get; set; }

        [Display(Name = "Cartes Jaunes")]
        public bool Jaunes { get; set; }

        [Display(Name = "Cartes Rouges")]
        public bool Rouges { get; set; }

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