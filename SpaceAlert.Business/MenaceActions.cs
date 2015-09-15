using SpaceAlert.Business;
using SpaceAlert.Model.Helpers.Enums;
using SpaceAlert.Model.Jeu;
using SpaceAlert.Model.Plateau;
using System;
using System.Linq;

namespace SpaceAlert.Business
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
        public static void Attack(InGameMenace source, Vaisseau target, TypeCase pallier, Zone from)
        {
            if (SpaceAlertData.Menace(source.MenaceName).MinDamages != null && SpaceAlertData.Menace(source.MenaceName).MinDamages.ContainsKey(pallier))
            {
                if (SpaceAlertData.Menace(source.MenaceName).MaxHp - source.CurrentHp < SpaceAlertData.Menace(source.MenaceName).MinDamages[pallier])
                {
                    return;
                }
            }
            InflictDamages(target, SpaceAlertData.Menace(source.MenaceName).AttackValues[pallier].First(), from);
        }

        /// <summary>
        /// Attaque toutes les zones à la fois
        /// </summary>
        /// <param name="source">La source de l'attaque</param>
        /// <param name="target">La cible de l'attaque</param>
        /// <param name="pallier">Le moment de l'attaque</param>
        /// <param name="from">La zone attaquée</param>
        public static void AttackOnAllZones(InGameMenace source, Vaisseau target, TypeCase pallier, Zone from)
        {
            Attack(source, target, pallier, from);
            AttackOnOtherZones(source, target, pallier, from);
        }

        /// <summary>
        /// Attaque les zones sur laquelle la InGameMenace ne se trouve pas
        /// </summary>
        /// <param name="source">La source de l'attaque</param>
        /// <param name="target">La cible de l'attaque</param>
        /// <param name="pallier">Le moment de l'attaque</param>
        /// <param name="from">La zone attaquée</param>
        public static void AttackOnOtherZones(InGameMenace source, Vaisseau target, TypeCase pallier, Zone from)
        {
            if (SpaceAlertData.Menace(source.MenaceName).MinDamages != null && SpaceAlertData.Menace(source.MenaceName).MinDamages.ContainsKey(pallier))
            {
                if (SpaceAlertData.Menace(source.MenaceName).MaxHp - source.CurrentHp < SpaceAlertData.Menace(source.MenaceName).MinDamages[pallier])
                {
                    return;
                }
            }
            foreach (Zone zone in target.Zones.Keys.Where(k => k != from))
            {
                InflictDamages(target, SpaceAlertData.Menace(source.MenaceName).AttackValues[pallier][1], zone);
            }
        }

        /// <summary>
        /// Inflige des dégâts proportionnels aux pv qu'il lui reste
        /// </summary>
        /// <param name="source">La source de l'attaque</param>
        /// <param name="target">La cible de l'attaque</param>
        /// <param name="pallier">Le moment de l'attaque</param>
        /// <param name="from">La zone attaquée</param>
        public static void InflictRemainingHitPoints(InGameMenace source, Vaisseau target, TypeCase pallier, Zone from)
        {
            InflictDamages(target, SpaceAlertData.Menace(source.MenaceName).AttackValues[pallier][0] * source.CurrentHp, from);
        }

        /// <summary>
        /// Augmente son shield
        /// </summary>
        /// <param name="source">La source de l'attaque</param>
        /// <param name="target">La cible de l'attaque</param>
        /// <param name="pallier">Le moment de l'attaque</param>
        /// <param name="from">La zone attaquée</param>
        public static void IncrShield(InGameMenace source, Vaisseau target, TypeCase pallier, Zone from)
        {
            source.CurrentShield += SpaceAlertData.Menace(source.MenaceName).ShieldValues[pallier];
        }

        /// <summary>
        /// Augmente sa vitesse
        /// </summary>
        /// <param name="source">La source de l'attaque</param>
        /// <param name="target">La cible de l'attaque</param>
        /// <param name="pallier">Le moment de l'attaque</param>
        /// <param name="from">La zone attaquée</param>
        public static void IncrSpeed(InGameMenace source, Vaisseau target, TypeCase pallier, Zone from)
        {
            source.CurrentSpeed += SpaceAlertData.Menace(source.MenaceName).SpeedValues[pallier];
        }

        /// <summary>
        /// Affecte son shield
        /// </summary>
        /// <param name="source">La source de l'attaque</param>
        /// <param name="target">La cible de l'attaque</param>
        /// <param name="pallier">Le moment de l'attaque</param>
        /// <param name="from">La zone attaquée</param>
        public static void SetShield(InGameMenace source, Vaisseau target, TypeCase pallier, Zone from)
        {
            source.CurrentShield = SpaceAlertData.Menace(source.MenaceName).ShieldValues[pallier];
        }

        /// <summary>
        /// Récupère la moitié de ses pv manquants
        /// </summary>
        /// <param name="source">La source de l'attaque</param>
        /// <param name="target">La cible de l'attaque</param>
        /// <param name="pallier">Le moment de l'attaque</param>
        /// <param name="from">La zone attaquée</param>
        public static void HealHalf(InGameMenace source, Vaisseau target, TypeCase pallier, Zone from)
        {
            source.CurrentHp += (SpaceAlertData.Menace(source.MenaceName).MaxHp - source.CurrentHp) / 2;
        }

        /// <summary>
        /// Récupère des points de vie
        /// </summary>
        /// <param name="source">La source de l'attaque</param>
        /// <param name="target">La cible de l'attaque</param>
        /// <param name="pallier">Le moment de l'attaque</param>
        /// <param name="from">La zone attaquée</param>
        public static void Heal(InGameMenace source, Vaisseau target, TypeCase pallier, Zone from)
        {
            source.CurrentHp = Math.Min(SpaceAlertData.Menace(source.MenaceName).MaxHp, source.CurrentHp + SpaceAlertData.Menace(source.MenaceName).HealValues[pallier]);
        }

        /// <summary>
        /// Se révèle
        /// </summary>
        /// <param name="source">La source de l'attaque</param>
        /// <param name="target">La cible de l'attaque</param>
        /// <param name="pallier">Le moment de l'attaque</param>
        /// <param name="from">La zone attaquée</param>
        public static void Reveals(InGameMenace source, Vaisseau target, TypeCase pallier, Zone from)
        {
            SpaceAlertData.Menace(source.MenaceName).Targetable = true;
        }

        /// <summary>
        /// Draine l'énergie de tous les shields
        /// </summary>
        /// <param name="source">La source de l'attaque</param>
        /// <param name="target">La cible de l'attaque</param>
        /// <param name="pallier">Le moment de l'attaque</param>
        /// <param name="from">La zone attaquée</param>
        public static void DrainsAllShields(InGameMenace source, Vaisseau target, TypeCase pallier, Zone from)
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
        public static void DrainShield(InGameMenace source, Vaisseau target, TypeCase pallier, Zone from)
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
