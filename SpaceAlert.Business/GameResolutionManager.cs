using SpaceAlert.Business.Factories;
using SpaceAlert.Model.Helpers;
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
        private GameContext game;

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
                    if (!game.Game.MenacesExternes.ContainsKey(evenement.Zone))
                    {
                        game.Game.MenacesExternes.Add(evenement.Zone, new List<InGameMenace>());
                    }
                    game.Game.MenacesExternes[evenement.Zone].Add(MenaceFactory.CreateMenace(game, evenement));
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
            ActionJoueur actionToResolve;
            do
            {
                // On retarde si la maintenance n'a pas été effectuée
                if (retardMaintenance)
                {
                    DelayPlayer(game.Game.Joueurs[indiceJoueurCourant], game.TourEnCours);
                }

                // On exécute l'action du joueur
                if (game.Game.Joueurs[indiceJoueurCourant].Actions.TryGetValue(game.TourEnCours, out actionToResolve) && actionToResolve != null)
                {
                    ResolveAction(game.Game.Joueurs[indiceJoueurCourant]);
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
        /// <param name="numTour"></param>
        private void ResolveAction(Joueur joueur)
        {
            ActionJoueur actionToResolve = joueur.Actions[game.TourEnCours];
            switch (actionToResolve.GenreAction)
            {
                case GenreAction.Action:
                    ResolveAction((TypeAction)actionToResolve.Value, joueur);
                    break;
                case GenreAction.Mouvement:
                    joueur.CurrentSalle = game.Game.Vaisseau.SalleSuivante(joueur.CurrentSalle, (Direction)actionToResolve.Value).Position;
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
            Salle currentSalle = game.Game.Vaisseau.Salle(source.CurrentSalle);
            switch (action)
            {
                case TypeAction.A:
                    Shoot(currentSalle);
                    break;
                case TypeAction.B:
                    if (source.CurrentSalle.Pont == Pont.HAUT)
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
            }
        }

        /// <summary>
        /// Résout une action C
        /// </summary>
        /// <param name="source"></param>
        private void ResolveCAction(Joueur source)
        {
            Salle playerSalle = game.Game.Vaisseau.Salle(source.CurrentSalle);
            switch (playerSalle.ActionC)
            {
                case CAction.ROBOTS:
                    if (playerSalle.HasRobots == PresenceRobots.PRESENTS && source.Robots == EtatRobots.NONE)
                    {
                        source.Robots = EtatRobots.ACTIF;
                        playerSalle.HasRobots = PresenceRobots.ACTIFS;
                    }
                    if (source.Robots == EtatRobots.CASSE && playerSalle.HasRobots != PresenceRobots.NONE)
                    {
                        source.Robots = EtatRobots.ACTIF;
                    }
                    break;
                case CAction.INTERCEPTEURS:
                    game.Game.Vaisseau.Interceptors = true;
                    break;
                case CAction.MAINTENANCE:
                    game.MaintenanceEffectuee = true;
                    break;
                case CAction.ROQUETTES:
                    if (!game.RoquettesNextTurn && game.Game.Vaisseau.NbRoquettes > 0)
                    {
                        game.RoquettesNextTurn = true;
                        game.Game.Vaisseau.NbRoquettes--;
                    }
                    break;
                case CAction.HUBLOT:
                    // TODO
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
            if (source.Canon.HasShot) return;

            source.Canon.HasShot = true;

            // Si c'est un canon qui consomme de l'énergie, on la déduit de la réserve
            if (source.Canon.ConsumeEnergy)
            {
                game.Game.Vaisseau.Salle(new Position(source.Position.Zone, Pont.BAS)).EnergieCourante--;
            }
        }

        /// <summary>
        /// Charge les boucliers
        /// </summary>
        /// <param name="source">La salle d'où est effectuée l'action</param>
        public void Shield(Salle source)
        {
            // TODO : vérifier si on peut le faire plusieurs fois
            int incrValue = Math.Min(source.EnergieMax - source.EnergieCourante, game.Game.Vaisseau.Salle(new Position(source.Position.Zone, Pont.BAS)).EnergieCourante);
            source.EnergieCourante += incrValue;
            game.Game.Vaisseau.Salle(new Position(source.Position.Zone, Pont.BAS)).EnergieCourante -= incrValue;
        }

        /// <summary>
        /// Charge les réacteurs
        /// </summary>
        /// <param name="source">La salle d'où est effectuée l'action</param>
        private void FillEnergy(Salle source)
        {
            // Cas de la salle centrale du bas
            if (source.Position.Zone == Zone.BLANCHE && game.Game.Vaisseau.NbCapsules > 0)
            {
                game.Game.Vaisseau.NbCapsules--;
                source.EnergieCourante = source.EnergieMax;
            }
            else
            {
                Position middleTop = new Position(Zone.BLANCHE, Pont.BAS);
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
            KeyValuePair<int, ActionJoueur> firstActionNull = joueur.Actions.FirstOrDefault(p => p.Key > numTour && p.Value == null);
            // bearg...
            int indexFirstNull = firstActionNull.Equals(default(KeyValuePair<int, ActionJoueur>)) ? game.Game.Mission.NbTours : firstActionNull.Key;

            for (int i = indexFirstNull; i > numTour; i--)
            {
                joueur.Actions[i] = joueur.Actions[i - 1];
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
            List<InGameMenace> targetableMenacesExternes = game.Game.MenacesExternes.SelectMany(m => m.Value).Where(m => ((MenaceExterne)m.Menace).RocketTargetable && m.Portee <= 2).ToList();
            if (targetableMenacesExternes.Any())
            {
                closerMenace = targetableMenacesExternes.OrderBy(m => m.Distance).First();
            }
            foreach (Zone zone in game.Game.Vaisseau.Zones.Keys)
            {
                int totalDamages = game.Game.Vaisseau.Zones[zone].Salles
                    .Select(s => s.Value.Canon)
                    .Where(c => c.HasShot)
                    .Sum(c => c.Power);

                if (game.RoquettesThisTurn && closerMenace != null && game.Game.MenacesExternes[zone].Contains(closerMenace))
                {
                    totalDamages += SpaceAlertData.RocketDamages;
                    game.RoquettesThisTurn = false;
                }

                if (game.Game.MenacesExternes.ContainsKey(zone))
                {
                    InGameMenace zoneMenace = game.Game.MenacesExternes[zone].OrderByDescending(m => m.Position).ThenBy(m => m.TourArrive).First();

                    // S'il y a une menace dans la zone on lui inflige des dégâts
                    if (zoneMenace != null)
                    {
                        Canon impulsionCanon = game.Game.Vaisseau.Salle(new Position(Zone.BLANCHE, Pont.BAS)).Canon;
                        if (zoneMenace.Portee <= impulsionCanon.Range && impulsionCanon.HasShot)
                        {
                            totalDamages += impulsionCanon.Power;
                        }
                        zoneMenace.DegatsSubis += totalDamages;
                        zoneMenace.CurrentHp = zoneMenace.CurrentHp - Math.Max(0, totalDamages - zoneMenace.Menace.Shield);
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
            foreach (Zone zone in game.Game.MenacesExternes.Keys)
            {
                foreach (InGameMenace menace in game.Game.MenacesExternes[zone].Where(m => m.Status == MenaceStatus.EnJeu))
                {
                    // On fait bouger la menace
                    int oldPos = menace.Position;
                    menace.Position += menace.CurrentSpeed;

                    // On résout ses actions si besoin
                    ResolveMenaceActions(menace, oldPos, menace.Position, zone);

                    // On sort la menace si elle atteint la fin de la rampe
                    if (menace.Position >= menace.Rampe.NbCases - 1)
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
            foreach (TypeCase typeCase in menace.Rampe.SpecialCases.Keys)
            {
                for (int i = 0; i < menace.Rampe.SpecialCases[typeCase].Count(v => v > oldPos && v <= newPos); i++)
                {
                    foreach (var action in menace.Menace.Actions[typeCase])
                    {
                        action(menace, game.Game.Vaisseau, typeCase, zone);
                    }
                }
            }
        }
    }
}
