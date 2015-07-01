using System.Collections.Generic;
using SpaceAlert.Model.Helpers.Enums;

namespace SpaceAlert.Model.Jeu
{
    public class InGameJoueur
    {
        public Joueur Joueur { get; set; }

        public List<ActionJoueur> Actions { get; set; }

        public EtatRobots Robots { get; set; }
    }
}
