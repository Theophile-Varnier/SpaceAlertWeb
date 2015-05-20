using SpaceAlert.Model.Helpers.Enums;
using SpaceAlert.Model.Menaces;
using SpaceAlert.Model.Plateau;
using System;
using System.Linq;

namespace SpaceAlert.Model.Helpers
{
    public static class MenaceActions
    {
        /// <summary>
        /// Attaque classique sur sa zone
        /// </summary>
        /// <param name="source">La source de l'attaque</param>
        /// <param name="target">La cible de l'attaque</param>
        /// <param name="pallier">Le moment de l'attaque</param>
        /// <param name="from">La zone attaquée</param>
        public static void Attack(Menace source, Vaisseau target, TypeCase pallier, Zone from)
        {
            if (source.MinDamages != null && source.MinDamages.ContainsKey(pallier))
            {
                if (source.MaxHp - source.CurrentHp < source.MinDamages[pallier])
                {
                    return;
                }
            }
            InflictDamages(target, source.AttackValues[pallier].First(), from);
        }

        /// <summary>
        /// Attaque toutes les zones à la fois
        /// </summary>
        /// <param name="source">La source de l'attaque</param>
        /// <param name="target">La cible de l'attaque</param>
        /// <param name="pallier">Le moment de l'attaque</param>
        /// <param name="from">La zone attaquée</param>
        public static void AttackOnAllZones(Menace source, Vaisseau target, TypeCase pallier, Zone from)
        {
            Attack(source, target, pallier, from);
            AttackOnOtherZones(source, target, pallier, from);
        }

        /// <summary>
        /// Attaque les zones sur laquelle la menace ne se trouve pas
        /// </summary>
        /// <param name="source">La source de l'attaque</param>
        /// <param name="target">La cible de l'attaque</param>
        /// <param name="pallier">Le moment de l'attaque</param>
        /// <param name="from">La zone attaquée</param>
        public static void AttackOnOtherZones(Menace source, Vaisseau target, TypeCase pallier, Zone from)
        {
            if (source.MinDamages != null && source.MinDamages.ContainsKey(pallier))
            {
                if (source.MaxHp - source.CurrentHp < source.MinDamages[pallier])
                {
                    return;
                }
            }
            foreach (Zone zone in target.Zones.Keys.Where(k => k != from))
            {
                InflictDamages(target, source.AttackValues[pallier][1], zone);
            }
        }

        /// <summary>
        /// Inflige des dégâts proportionnels aux pv qu'il lui reste
        /// </summary>
        /// <param name="source">La source de l'attaque</param>
        /// <param name="target">La cible de l'attaque</param>
        /// <param name="pallier">Le moment de l'attaque</param>
        /// <param name="from">La zone attaquée</param>
        public static void InflictRemainingHitPoints(Menace source, Vaisseau target, TypeCase pallier, Zone from)
        {
            InflictDamages(target, source.AttackValues[pallier][0] * source.CurrentHp, from);
        }

        /// <summary>
        /// Augmente son shield
        /// </summary>
        /// <param name="source">La source de l'attaque</param>
        /// <param name="target">La cible de l'attaque</param>
        /// <param name="pallier">Le moment de l'attaque</param>
        /// <param name="from">La zone attaquée</param>
        public static void IncrShield(Menace source, Vaisseau target, TypeCase pallier, Zone from)
        {
            source.Shield += source.ShieldValues[pallier];
        }

        /// <summary>
        /// Augmente sa vitesse
        /// </summary>
        /// <param name="source">La source de l'attaque</param>
        /// <param name="target">La cible de l'attaque</param>
        /// <param name="pallier">Le moment de l'attaque</param>
        /// <param name="from">La zone attaquée</param>
        public static void IncrSpeed(Menace source, Vaisseau target, TypeCase pallier, Zone from)
        {
            source.Speed += source.SpeedValues[pallier];
        }

        /// <summary>
        /// Affecte son shield
        /// </summary>
        /// <param name="source">La source de l'attaque</param>
        /// <param name="target">La cible de l'attaque</param>
        /// <param name="pallier">Le moment de l'attaque</param>
        /// <param name="from">La zone attaquée</param>
        public static void SetShield(Menace source, Vaisseau target, TypeCase pallier, Zone from)
        {
            source.Shield = source.ShieldValues[pallier];
        }

        /// <summary>
        /// Récupère la moitié de ses pv manquants
        /// </summary>
        /// <param name="source">La source de l'attaque</param>
        /// <param name="target">La cible de l'attaque</param>
        /// <param name="pallier">Le moment de l'attaque</param>
        /// <param name="from">La zone attaquée</param>
        public static void HealHalf(Menace source, Vaisseau target, TypeCase pallier, Zone from)
        {
            source.CurrentHp += (source.MaxHp - source.CurrentHp) / 2;
        }

        /// <summary>
        /// Récupère des points de vie
        /// </summary>
        /// <param name="source">La source de l'attaque</param>
        /// <param name="target">La cible de l'attaque</param>
        /// <param name="pallier">Le moment de l'attaque</param>
        /// <param name="from">La zone attaquée</param>
        public static void Heal(Menace source, Vaisseau target, TypeCase pallier, Zone from)
        {
            source.CurrentHp = Math.Min(source.MaxHp, source.CurrentHp + source.HealValues[pallier]);
        }

        /// <summary>
        /// Se révèle
        /// </summary>
        /// <param name="source">La source de l'attaque</param>
        /// <param name="target">La cible de l'attaque</param>
        /// <param name="pallier">Le moment de l'attaque</param>
        /// <param name="from">La zone attaquée</param>
        public static void Reveals(Menace source, Vaisseau target, TypeCase pallier, Zone from)
        {
            source.Targetable = true;
        }

        /// <summary>
        /// Draine l'énergie de tous les shields
        /// </summary>
        /// <param name="source">La source de l'attaque</param>
        /// <param name="target">La cible de l'attaque</param>
        /// <param name="pallier">Le moment de l'attaque</param>
        /// <param name="from">La zone attaquée</param>
        public static void DrainsAllShields(Menace source, Vaisseau target, TypeCase pallier, Zone from)
        {
            foreach (Zone z in target.Zones.Keys)
            {
                DrainShield(source, target, pallier, z);
            }
        }

        /// <summary>
        /// Draine l'énergie de sa zone
        /// </summary>
        /// <param name="source">La source de l'attaque</param>
        /// <param name="target">La cible de l'attaque</param>
        /// <param name="pallier">Le moment de l'attaque</param>
        /// <param name="from">La zone attaquée</param>
        public static void DrainShield(Menace source, Vaisseau target, TypeCase pallier, Zone from)
        {
            target.Zones[from].Salles[Pont.HAUT].EnergieCourante = 0;
        }

        /// <summary>
        /// Inflige un certain nombre de dégats à une zone
        /// </summary>
        /// <param name="target"></param>
        /// <param name="damageValue"></param>
        /// <param name="from"></param>
        private static void InflictDamages(Vaisseau target, int damageValue, Zone from)
        {
            // dégats non absorbés par le bouclier
            int degatsRestants = damageValue - target.Zones[from].Salles[Pont.HAUT].EnergieCourante;

            if (degatsRestants > 0)
            {
                target.Zones[from].Degats += degatsRestants;
                target.Zones[from].Salles[Pont.HAUT].EnergieCourante = 0;
            }
            else
            {
                target.Zones[from].Salles[Pont.HAUT].EnergieCourante = Math.Abs(degatsRestants);
            }
        }
    }
}
