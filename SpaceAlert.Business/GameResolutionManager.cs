﻿using SpaceAlert.Model.Helpers;
using SpaceAlert.Model.Helpers.Enums;
using SpaceAlert.Model.Jeu;
using SpaceAlert.Model.Jeu.Evenements;
using SpaceAlert.Model.Menaces;
using SpaceAlert.Model.Plateau;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SpaceAlert.Business
{
    /// <summary>
    /// Manager de parties
    /// </summary>
    public class GameResolutionManager
    {
        private readonly GameContext game;

        public GameResolutionManager(GameContext gameContext)
        {
            game = gameContext;
        }

        /// <summary>
        /// Gestion de l'arrivée dans une nouvelle phase
        /// </summary>
        private void InitPhase()
        {
            // TODO : points de hublot
            game.MaintenanceEffectuee = false;
        }

        /// <summary>
        /// Résout une partie
        /// </summary>
        public void Resolve()
        {
            while (game.TourEnCours <= game.Game.Mission.NbTours)
            {
                ResolveTurn();
            }
        }

        /// <summary>
        /// Résout un tour
        /// </summary>
        public void ResolveTurn()
        {
            if (game.TourEnCours > game.Game.Mission.NbTours)
            {
                return;
            }
            game.RoquettesThisTurn = game.RoquettesNextTurn;
            game.RoquettesNextTurn = false;

            // Gestion si nouvelle phase
            if (SpaceAlertData.DebutPhases.Contains(game.TourEnCours))
            {
                InitPhase();
            }

            // Apparition des menaces
            foreach (EvenementMenace evenement in game.Game.Mission.Evenements.OfType<EvenementMenace>())
            {
                if (evenement.TourArrive == game.TourEnCours)
                {
                    game.Game.MenacesExternes.Single(m => m.AnnonceEvenement == evenement.Annonce).Status = MenaceStatus.EnJeu;
                }
            }

            // On vérifie que la maintenance a été effectuée
            bool retardMaintenance = SpaceAlertData.DebutPhases.Select(i => i + 2).Contains(game.TourEnCours) && !game.MaintenanceEffectuee;

            // Récupération de l'indice du premier joueur à jouer (le capitaine)
            int indicePremierJoueur = game.Game.Joueurs.FirstIndexOf(j => j.IsCapitaine);

            // Paye ta gestion d'erreur...
            if (indicePremierJoueur == -1)
            {
                game.TourEnCours++;
                return;
            }

            // Résolution des actions des joueurs
            int indiceJoueurCourant = indicePremierJoueur;
            ActionInTour actionToResolve;
            do
            {
                Joueur currentPlayer = game.Game.Joueurs[indiceJoueurCourant];
                // On retarde si la maintenance n'a pas été effectuée
                if (retardMaintenance)
                {
                    DelayPlayer(currentPlayer, game.TourEnCours);
                }

                // On exécute l'action du joueur
                if (currentPlayer.Status != StatusJoueur.Assomme && (actionToResolve = currentPlayer.Actions.FirstOrDefault(a => a.Tour == game.TourEnCours)) != null)
                {
                    ResolveAction(currentPlayer);
                }

                indiceJoueurCourant = (indiceJoueurCourant + 1) % game.Game.Joueurs.Count;
            } while (indiceJoueurCourant != indicePremierJoueur);

            // Résolution des tirs
            ResolveShoots();

            // TODO : résolution actions menaces internes
            ResolveMenaceActions();

            game.TourEnCours++;
        }

        /// <summary>
        /// Résout une action d'un joueur
        /// </summary>
        /// <param name="joueur"></param>
        private void ResolveAction(Joueur joueur)
        {
            ActionJoueur actionToResolve = joueur.Actions.Single(a => a.Tour == game.TourEnCours).Action;
            switch (actionToResolve.GenreAction)
            {
                case GenreAction.Action:
                    TypeAction action = (TypeAction)actionToResolve.Value;
                    ResolveAction(action, joueur);
                    break;
                case GenreAction.Mouvement:
                    if (joueur.Status == StatusJoueur.EnJeu)
                    {
                        joueur.CurrentSalle = game.Game.Vaisseau.SalleSuivante(joueur.CurrentSalle, (Direction)actionToResolve.Value).Position;
                    }
                    break;
                default:
                    // do the default action
                    break;
            }
        }

        /// <summary>
        /// Résout une action selon son type
        /// </summary>
        /// <param name="action">l'action à résoudre</param>
        /// <param name="source">Le joueur qui effectue l'action</param>
        private void ResolveAction(TypeAction action, Joueur source)
        {
            // On vérifie que l'action est autorisée dans la partie en cours
            if (!game.Config.AllowedActions.HasFlag(action))
            {
                return;
            }

            Salle currentSalle = game.Game.Vaisseau.Salle(source.CurrentSalle);

            switch (action)
            {
                case TypeAction.A:
                    Shoot(currentSalle);
                    break;
                case TypeAction.B:
                    if (source.CurrentSalle.Pont == Pont.Haut)
                    {
                        Shield(currentSalle);
                    }
                    else
                    {
                        FillEnergy(currentSalle);
                    }
                    break;
                case TypeAction.C:
                    ResolveCAction(source);
                    break;
                case TypeAction.Robots:
                    // TODO : Menaces internes
                    break;
                default:
                    // do the default action
                    break;
            }
        }

        /// <summary>
        /// Résout une action C
        /// </summary>
        /// <param name="source"></param>
        private void ResolveCAction(Joueur source)
        {
            Salle playerSalle = game.Game.Vaisseau.Salle(source.CurrentSalle);

            // On vérifie que l'action est autorisée dans la partie actuelle
            if (!game.Config.AllowedActionsC.HasFlag(playerSalle.ActionC))
            {
                return;
            }
            switch (playerSalle.ActionC)
            {
                case ActionC.Robots:
                    if (playerSalle.HasRobots == PresenceRobots.Presents && source.Robots == EtatRobots.None)
                    {
                        source.Robots = EtatRobots.Actif;
                        playerSalle.HasRobots = PresenceRobots.Actifs;
                    }
                    if (source.Robots == EtatRobots.Casse && playerSalle.HasRobots != PresenceRobots.None)
                    {
                        source.Robots = EtatRobots.Actif;
                    }
                    break;
                case ActionC.Intercepteurs:
                    source.Status = StatusJoueur.Intercepteurs;
                    break;
                case ActionC.Maintenance:
                    game.MaintenanceEffectuee = true;
                    break;
                case ActionC.Roquettes:
                    if (!game.RoquettesNextTurn && game.Game.Vaisseau.NbRoquettes > 0)
                    {
                        game.RoquettesNextTurn = true;
                        game.Game.Vaisseau.NbRoquettes--;
                    }
                    break;
                case ActionC.Hublot:
                    // TODO
                    break;
                default:
                    // do the default action
                    break;
            }
        }

        /// <summary>
        /// Tire sur une menace
        /// </summary>
        /// <param name="source">La salle d'où est effectuée l'action</param>
        private void Shoot(Salle source)
        {
            // Si le canon a déjà tiré, il ne se passe rien
            if (source.Canon.HasShot)
            {
                return;
            }


            source.Canon.HasShot = true;

            // Si c'est un canon qui consomme de l'énergie, on la déduit de la réserve
            if (source.Canon.ConsumeEnergy)
            {
                game.Game.Vaisseau.Salle(new Position(source.Position.Zone, Pont.Bas)).EnergieCourante--;
            }
        }

        /// <summary>
        /// Charge les boucliers
        /// </summary>
        /// <param name="source">La salle d'où est effectuée l'action</param>
        public void Shield(Salle source)
        {
            // TODO : vérifier si on peut le faire plusieurs fois
            int incrValue = Math.Min(source.EnergieMax - source.EnergieCourante, game.Game.Vaisseau.Salle(new Position(source.Position.Zone, Pont.Bas)).EnergieCourante);
            source.EnergieCourante += incrValue;
            game.Game.Vaisseau.Salle(new Position(source.Position.Zone, Pont.Bas)).EnergieCourante -= incrValue;
        }

        /// <summary>
        /// Charge les réacteurs
        /// </summary>
        /// <param name="source">La salle d'où est effectuée l'action</param>
        private void FillEnergy(Salle source)
        {
            // Cas de la salle centrale du bas
            if (source.Position.Zone == Zone.Blanche && game.Game.Vaisseau.NbCapsules > 0)
            {
                game.Game.Vaisseau.NbCapsules--;
                source.EnergieCourante = source.EnergieMax;
            }
            else
            {
                Position middleTop = new Position(Zone.Blanche, Pont.Bas);
                int incrValue = Math.Min(game.Game.Vaisseau.Salle(middleTop).EnergieCourante, source.EnergieMax - source.EnergieCourante);
                game.Game.Vaisseau.Salle(middleTop).EnergieCourante -= incrValue;
                source.EnergieCourante += incrValue;
            }
        }

        /// <summary>
        /// Retarde un joueur
        /// </summary>
        /// <param name="joueur">Le joueur à retarder</param>
        /// <param name="numTour">le tour à partir duquel on le retarde</param>
        private void DelayPlayer(Joueur joueur, int numTour)
        {
            ActionInTour firstActionNull = joueur.Actions.OrderBy(a => a.Tour).FirstOrDefault(p => p.Tour > numTour && p.Action == null);
            // bearg...
            int indexFirstNull = firstActionNull == null ? game.Game.Mission.NbTours : firstActionNull.Tour;

            for (int i = indexFirstNull; i > numTour; i--)
            {
                joueur.Actions.Single(a => a.Tour == i).Action = joueur.Actions.Single(a => a.Tour == i - 1).Action;
            }
            joueur.Actions[numTour] = null;
        }

        /// <summary>
        /// Résout tous les tirs du tour
        /// </summary>
        private void ResolveShoots()
        {
            // On récupère les menaces ciblables par les roquettes
            InGameMenace closerMenace = null;
            List<InGameMenace> targetableMenacesExternes = game.Game.MenacesExternes.Where(m => ((MenaceExterne)SpaceAlertData.Menace(m.MenaceName)).RocketTargetable && MenacePortee(m) <= 2).ToList();
            if (targetableMenacesExternes.Any())
            {
                closerMenace = targetableMenacesExternes.OrderBy(MenaceDistance).First();
            }
            foreach (Zone zone in game.Game.Vaisseau.Zones.Select(z => z.Zone).Distinct())
            {
                int totalDamages = game.Game.Vaisseau.Zones.Single(z => z.Zone == zone).Salles
                    .Select(s => s.Canon)
                    .Where(c => c.HasShot)
                    .Sum(c => c.Power);

                if (game.RoquettesThisTurn && closerMenace != null && game.Game.MenacesExternes.Where(m => m.Zone == zone).Contains(closerMenace))
                {
                    totalDamages += SpaceAlertData.RocketDamages;
                    game.RoquettesThisTurn = false;
                }

                if (game.Game.MenacesExternes.Any(m => m.Zone == zone))
                {
                    InGameMenace zoneMenace = game.Game.MenacesExternes.Where(m => m.Zone == zone).OrderByDescending(m => m.Position).ThenBy(m => m.TourArrive).First();

                    // S'il y a une menace dans la zone on lui inflige des dégâts
                    if (zoneMenace != null)
                    {
                        Canon impulsionCanon = game.Game.Vaisseau.Salle(new Position(Zone.Blanche, Pont.Bas)).Canon;
                        if (MenacePortee(zoneMenace) <= impulsionCanon.Range && impulsionCanon.HasShot)
                        {
                            totalDamages += impulsionCanon.Power;
                        }
                        zoneMenace.DegatsSubis += totalDamages;
                        zoneMenace.CurrentHp = zoneMenace.CurrentHp - Math.Max(0, totalDamages - SpaceAlertData.Menace(zoneMenace.MenaceName).Shield);
                        if (zoneMenace.CurrentHp <= 0)
                        {
                            zoneMenace.Status = MenaceStatus.Detruite;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Résout les actions des menaces présentes
        /// </summary>
        private void ResolveMenaceActions()
        {
            IEnumerable<Zone> zones = game.Game.MenacesExternes.Select(m => m.Zone).Distinct();
            foreach (Zone zone in zones)
            {
                IEnumerable<InGameMenace> activesMenacesInZone = game.Game.MenacesExternes.Where(m => m.Zone == zone).Where(m => m.Status == MenaceStatus.EnJeu);
                foreach (InGameMenace menace in activesMenacesInZone)
                {
                    // On fait bouger la menace
                    int oldPos = menace.Position;
                    menace.Position += menace.CurrentSpeed;

                    // On résout ses actions si besoin
                    ResolveMenaceActions(menace, oldPos, menace.Position, zone);

                    // On sort la menace si elle atteint la fin de la rampe
                    if (menace.Position >= SpaceAlertData.Rampe(menace.RampeId).NbCases - 1)
                    {
                        menace.Status = MenaceStatus.Survecue;
                    }
                }
            }
        }

        /// <summary>
        /// Execute les actions d'une menace lors d'un déplacement
        /// </summary>
        /// <param name="menace">La menace</param>
        /// <param name="oldPos">L'ancienne position</param>
        /// <param name="newPos">La nouvelle position</param>
        /// <param name="zone">La zone où se situe la menace</param>
        private void ResolveMenaceActions(InGameMenace menace, int oldPos, int newPos, Zone zone)
        {
            Rampe menaceRampe = SpaceAlertData.Rampe(menace.RampeId);
            foreach (TypeCase typeCase in menaceRampe.SpecialCases.Keys)
            {
                for (int i = 0; i < menaceRampe.SpecialCases[typeCase].Count(v => v > oldPos && v <= newPos); i++)
                {
                    Menace currentMenace = SpaceAlertData.Menace(menace.MenaceName);
                    foreach (var action in currentMenace.Actions[typeCase])
                    {
                        action(menace, game.Game.Vaisseau, typeCase, zone);
                    }
                }
            }
        }

        /// <summary>
        /// Récupère la distance à laquelle se trouve une menace
        /// </summary>
        /// <param name="menace">The menace.</param>
        /// <returns></returns>
        private int MenaceDistance(InGameMenace menace)
        {
            return SpaceAlertData.Rampe(menace.RampeId).NbCases - menace.Position;
        }

        /// <summary>
        /// Récupère la portée à laquelle se trouve une menace
        /// </summary>
        /// <param name="menace">The menace.</param>
        /// <returns></returns>
        private int MenacePortee(InGameMenace menace)
        {
            return MenaceDistance(menace) / 5 + 1;
        }
    }
}
