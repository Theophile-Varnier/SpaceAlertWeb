using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SpaceAlert.Model.Helpers.Enums;

namespace SpaceAlert.Web.Models
{
    public class GameViewModel
    {
        public TypeMission TypeMission { get; set; }

        public int NbJoueurs { get; set; }

        public int NbAndroids { get; set; }

        public bool Blanches { get; set; }

        public bool Jaunes { get; set; }

        public bool Rouges { get; set; }
    }
}