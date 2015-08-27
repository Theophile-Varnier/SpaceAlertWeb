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
    public class GameManager
    {
        private GameContext game;

        public GameManager(GameContext gameContext)
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
            while (game.TourEnCours < game.Partie.Mission.NbTours)
            {
                ResolveTurn();
            }
        }

        /// <summary>
        /// Résout un tour
        /// </summary>
        /// <param name="numTour"></param>
        public void ResolveTurn()
        {

            if (game.TourEnCours > game.Partie.Mission.NbTours)
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
            foreach (EvenementMenace evenement in game.Partie.Mission.Evenements.OfType<EvenementMenace>())
            {
                if (evenement.TourArrive == game.TourEnCours)
                {
                    if (!game.Partie.MenacesExternes.ContainsKey(evenement.Zone))
                    {
                        game.Partie.MenacesExternes.Add(evenement.Zone, new List<InGameMenace>());
                    }
                    game.Partie.MenacesExternes[evenement.Zone].Add(new InGameMenace
                    {
                        Menace = evenement.Menace,
                        CurrentHp = evenement.Menace.MaxHp,
                        Position = 0,
                        TourArrive = game.TourEnCours,
                        DegatsSubis = 0,
                        Rampe = game.Rampes[evenement.Zone]
                    });
                }
            }

            // On vérifie que la maintenance a été effectuée
            bool retardMaintenance = SpaceAlertData.DebutPhases.Select(i => i + 2).Contains(game.TourEnCours) && !game.MaintenanceEffectuee;

            // Récupération de l'indice du premier joueur à jouer (le capitaine)
            int indicePremierJoueur = game.Partie.Joueurs.FirstIndexOf(j => j.IsCapitaine);

            // Paye ta gestion d'erreur...
            if (indicePremierJoueur == -1)
            {
                game.TourEnCours++;
                return;
            }

            // Résolution des actions des joueurs
            for (int i = indicePremierJoueur; i != indicePremierJoueur; i = (i + 1) % game.Partie.Joueurs.Count)
            {
                // On retarde si la maintenance n'a pas été effectuée
                if (retardMaintenance)
                {
                    DelayPlayer(game.Partie.Joueurs[i], game.TourEnCours);
                }

                // On exécute l'action du joueur
                if (game.Partie.Joueurs[i].Actions[game.TourEnCours] != null)
                {
                    ResolveAction(game.Partie.Joueurs[i], game.TourEnCours);
                }
            }

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
        private void ResolveAction(Joueur joueur, int numTour)
        {
            ActionJoueur actionToResolve = joueur.Actions[numTour];
            switch (actionToResolve.GenreAction)
            {
                case GenreAction.Action:
                    ResolveAction((TypeAction)actionToResolve.Value, joueur);
                    break;
                case GenreAction.Mouvement:
                    joueur.CurrentSalle = game.Partie.Vaisseau.SalleSuivante(joueur.CurrentSalle, (Direction)actionToResolve.Value);
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
            switch (action)
            {
                case TypeAction.A:
                    Shoot(source.CurrentSalle);
                    break;
                case TypeAction.B:
                    if (source.CurrentSalle.Pont == Pont.HAUT)
                    {
                        Shield(source.CurrentSalle);
                    }
                    else
                    {
                        FillEnergy(source.CurrentSalle);
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
            switch (source.CurrentSalle.ActionC)
            {
                case CAction.ROBOTS:
                    if (source.CurrentSalle.HasRobots == PresenceRobots.PRESENTS && source.Robots == EtatRobots.NONE)
                    {
                        source.Robots = EtatRobots.ACTIF;
                        source.CurrentSalle.HasRobots = PresenceRobots.ACTIFS;
                    }
                    if (source.Robots == EtatRobots.CASSE && source.CurrentSalle.HasRobots != PresenceRobots.NONE)
                    {
                        source.Robots = EtatRobots.ACTIF;
                    }
                    break;
                case CAction.INTERCEPTEURS:
                    game.Partie.Vaisseau.Interceptors = true;
                    break;
                case CAction.MAINTENANCE:
                    game.MaintenanceEffectuee = true;
                    break;
                case CAction.ROQUETTES:
                    if (!game.RoquettesNextTurn && game.Partie.Vaisseau.NbRoquettes > 0)
                    {
                        game.RoquettesNextTurn = true;
                        game.Partie.Vaisseau.NbRoquettes--;
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
                game.Partie.Vaisseau.Salle(source.Zone, Pont.BAS).EnergieCourante--;
            }
        }

        /// <summary>
        /// Charge les boucliers
        /// </summary>
        /// <param name="source">La salle d'où est effectuée l'action</param>
        public void Shield(Salle source)
        {
            // TODO : vérifier si on peut le faire plusieurs fois
            int incrValue = Math.Min(source.EnergieMax - source.EnergieCourante, game.Partie.Vaisseau.Salle(source.Zone, Pont.BAS).EnergieCourante);
            source.EnergieCourante += incrValue;
            game.Partie.Vaisseau.Salle(source.Zone, Pont.BAS).EnergieCourante -= incrValue;
        }

        /// <summary>
        /// Charge les réacteurs
        /// </summary>
        /// <param name="source">La salle d'où est effectuée l'action</param>
        private void FillEnergy(Salle source)
        {
            // Cas de la salle centrale du bas
            if (source.Zone == Zone.BLANCHE && game.Partie.Vaisseau.NbCapsules > 0)
            {
                game.Partie.Vaisseau.NbCapsules--;
                source.EnergieCourante = source.EnergieMax;
            }
            else
            {
                int incrValue = Math.Min(game.Partie.Vaisseau.Salle(Zone.BLANCHE, Pont.BAS).EnergieCourante, source.EnergieMax - source.EnergieCourante);
                game.Partie.Vaisseau.Salle(Zone.BLANCHE, Pont.BAS).EnergieCourante -= incrValue;
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
            int indexFirstNull = firstActionNull.Equals(default(KeyValuePair<int, ActionJoueur>)) ? game.Partie.Mission.NbTours : firstActionNull.Key;

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
            List<InGameMenace> targetableMenacesExternes = game.Partie.MenacesExternes.SelectMany(m => m.Value).Where(m => ((MenaceExterne)m.Menace).RocketTargetable && m.Portee <= 2).ToList();
            InGameMenace closerMenace = targetableMenacesExternes.OrderBy(m => m.Distance).First();
            foreach (Zone zone in game.Partie.Vaisseau.Zones.Keys)
            {
                int totalDamages = game.Partie.Vaisseau.Zones[zone].Salles
                    .Select(s => s.Value.Canon)
                    .Where(c => c.HasShot)
                    .Sum(c => c.Power);

                if (game.RoquettesThisTurn && game.Partie.MenacesExternes[zone].Contains(closerMenace))
                {
                    totalDamages += SpaceAlertData.RocketDamages;
                    game.RoquettesThisTurn = false;
                }

                InGameMenace zoneMenace = game.Partie.MenacesExternes[zone].OrderByDescending(m => m.Position).ThenBy(m => m.TourArrive).First();

                // S'il y a une menace dans la zone on lui inflige des dégâts
                if (zoneMenace != null)
                {
                    Canon impulsionCanon = game.Partie.Vaisseau.Salle(Zone.BLANCHE, Pont.BAS).Canon;
                    if (zoneMenace.Portee <= impulsionCanon.Range && impulsionCanon.HasShot)
                    {
                        totalDamages += impulsionCanon.Power;
                    }
                    zoneMenace.DegatsSubis += totalDamages;
                    zoneMenace.CurrentHp = Math.Max(0, zoneMenace.CurrentHp - Math.Max(0, totalDamages - zoneMenace.Menace.Shield));
                    //if (zoneMenace.CurrentHp == 0)
                    //{
                    //    game.Partie.MenacesExternes[zone].Remove(zoneMenace);
                    //    game.MenacesDetruites.Add(zoneMenace.Menace);
                    //}
                }
            }
        }

        /// <summary>
        /// Résout les actions des menaces présentes
        /// </summary>
        private void ResolveMenaceActions()
        {
            foreach (Zone zone in game.Partie.MenacesExternes.Keys)
            {
                foreach (InGameMenace menace in game.Partie.MenacesExternes[zone])
                {
                    // On fait bouger la menace
                    int oldPos = menace.Position;
                    menace.Position += menace.Menace.Speed;

                    // On résout ses actions si besoin
                    ResolveMenaceActions(menace, oldPos, menace.Position, zone);

                    // On sort la menace si elle atteint la fin de la rampe
                    //if (menace.Position >= menace.Rampe.NbCases - 1)
                    //{
                    //    game.MenacesSurvecues.Add(menace.Menace);
                    //}
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
                        action(menace, game.Partie.Vaisseau, typeCase, zone);
                    }
                }
            }
        }
    }
}
