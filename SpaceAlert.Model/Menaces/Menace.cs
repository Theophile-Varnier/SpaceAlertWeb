﻿using SpaceAlert.Model.Helpers.Enums;
using SpaceAlert.Model.Plateau;
using System;
using System.Collections.Generic;

namespace SpaceAlert.Model.Menaces
{
    /// <summary>
    /// Décrit une menace en jeu
    /// </summary>
    public class Menace
    {

        /// <summary>
        /// Le nom de la menace
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// La valeur de bouclier de la menace
        /// </summary>
        public int Shield { get; set; }

        /// <summary>
        /// Le nombre de pv actuel de la menace
        /// </summary>
        public int CurrentHp { get; set; }

        /// <summary>
        /// Le nombre max de pv de la menace
        /// </summary>
        public int MaxHp { get; set; }

        /// <summary>
        /// La vitesse de déplacement de la menace
        /// </summary>
        public int Speed { get; set; }

        /// <summary>
        /// Liste des actions effectuées par la menace
        /// lorsqu'elle arrive sur une case X
        /// </summary>
        public IList<Action<Menace, Vaisseau, Zone>> ActionsX { get; set; }

        /// <summary>
        /// Liste des actions effectuées par la menace
        /// lorsqu'elle arrive sur une case Y
        /// </summary>
        public IList<Action<Menace, Vaisseau, Zone>> ActionsY { get; set; }

        /// <summary>
        /// Liste des actions effectuées par la menace
        /// lorsqu'elle arrive sur une case Z
        /// </summary>
        public IList<Action<Menace, Vaisseau, Zone>> ActionsZ { get; set; }
    }
}
