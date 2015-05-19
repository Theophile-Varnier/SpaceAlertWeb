using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SpaceAlert.Model.Helpers.Enums;

namespace SpaceAlert.Web.Models
{
    public class GameViewModel : AbstractViewModel
    {
        public TypeMission TypeMission { get; set; }

        public int NbJoueurs { get; set; }

        public int NbAndroids { get; set; }

        public bool Blanches { get; set; }

        public bool Jaunes { get; set; }

        public bool Rouges { get; set; }

        public override bool IsValid()
        {
            if (!Blanches && !Jaunes && !Rouges)
            {
                return false;
            }
            if (NbJoueurs < 0 || NbJoueurs > 5)
            {
                return false;
            }
            if (NbAndroids < 0 || NbAndroids > 4)
            {
                return false;
            }
            return NbAndroids + NbJoueurs <= 5;
        }
    }
}