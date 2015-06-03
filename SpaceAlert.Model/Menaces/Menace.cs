using SpaceAlert.Model.Helpers.Enums;
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
        /// Les valeurs d'attaque (peu importe le type)
        /// </summary>
        public Dictionary<TypeCase, List<int>> AttackValues { get; set; }

        /// <summary>
        /// Les valeurs de bouclier (full ou incrémental)
        /// </summary>
        public Dictionary<TypeCase, int> ShieldValues { get; set; }

        /// <summary>
        /// Les valeurs de heal
        /// </summary>
        public Dictionary<TypeCase, int> HealValues { get; set; }

        /// <summary>
        /// Les valeurs de speed
        /// </summary>
        public Dictionary<TypeCase, int> SpeedValues { get; set; }

        /// <summary>
        /// Les valeurs de dégâts mininum
        /// </summary>
        public Dictionary<TypeCase, int> MinDamages { get; set; }

        /// <summary>
        /// Indique si la menace est ciblable par les canons
        /// </summary>
        public bool Targetable { get; set; }

        /// <summary>
        /// Liste des actions effectuées par la menace
        /// lorsqu'elle arrive sur une case X
        /// </summary>
        public IList<Action<Menace, Vaisseau, TypeCase, Zone>> ActionsX { get; set; }

        /// <summary>
        /// Liste des actions effectuées par la menace
        /// lorsqu'elle arrive sur une case Y
        /// </summary>
        public IList<Action<Menace, Vaisseau, TypeCase, Zone>> ActionsY { get; set; }

        /// <summary>
        /// Liste des actions effectuées par la menace
        /// lorsqu'elle arrive sur une case Z
        /// </summary>
        public IList<Action<Menace, Vaisseau, TypeCase, Zone>> ActionsZ { get; set; }
    }
}
