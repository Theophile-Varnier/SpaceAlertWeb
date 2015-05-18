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
        /// Constructeur par recopie
        /// </summary>
        /// <param name="source">La menace à copier</param>
        //public Menace(Menace source) 
        //{
        //    Name = source.Name;
        //    MaxHp = source.MaxHp;
        //    CurrentHp = MaxHp;
        //    Speed = source.Speed;
        //    Targetable = source.Targetable;
        //    AttackValues = new Dictionary<TypeCase,List<int>>();
        //    foreach (TypeCase c in source.AttackValues.Keys)
        //    {
        //        AttackValues.Add(c, new List<int>());
        //        foreach (int val in source.AttackValues[c])
        //        {
        //            AttackValues[c].Add(val);
        //        }
        //    }
        //    ShieldValues = new Dictionary<TypeCase, int>();
        //    foreach (TypeCase c in source.ShieldValues.Keys)
        //    {
        //        ShieldValues.Add(c, source.ShieldValues[c]);
        //    }

        //    SpeedValues = new Dictionary<TypeCase, int>();
        //    foreach (TypeCase c in source.SpeedValues.Keys)
        //    {
        //        SpeedValues.Add(c, source.SpeedValues[c]);
        //    }

        //    HealValues = new Dictionary<TypeCase, int>();
        //    foreach (TypeCase c in source.HealValues.Keys)
        //    {
        //        HealValues.Add(c, source.HealValues[c]);
        //    }

        //    MinDamages = new Dictionary<TypeCase, int>();
        //    foreach (TypeCase c in source.MinDamages.Keys)
        //    {
        //        MinDamages.Add(c, source.MinDamages[c]);
        //    }

        //    ActionsX = new List<Action<Menace, Vaisseau, TypeCase, Zone>>();
        //    foreach (var action in source.ActionsX)
        //    {
        //        ActionsX.Add(action);
        //    }

        //    ActionsY = new List<Action<Menace, Vaisseau, TypeCase, Zone>>();
        //    foreach (var action in source.ActionsY)
        //    {
        //        ActionsY.Add(action);
        //    }

        //    ActionsZ = new List<Action<Menace, Vaisseau, TypeCase, Zone>>();
        //    foreach (var action in source.ActionsZ)
        //    {
        //        ActionsZ.Add(action);
        //    }
        //}

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
