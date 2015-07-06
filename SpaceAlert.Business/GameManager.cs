using SpaceAlert.Model.Jeu;
using SpaceAlert.Model.Helpers;
using System.Linq;
using SpaceAlert.Model.Helpers.Enums;
using SpaceAlert.Model.Plateau;
using System;

namespace SpaceAlert.Business
{
    /// <summary>
    /// Manager de parties
    /// TODO : Refacto : tous les tirs doivent se résoudre en même temps
    /// </summary>
    public class GameManager
    {
        private GameContext game;

        public GameManager(GameContext gameContext)
        {
            game = gameContext;
        }

        /// <summary>
        /// Résout une partie
        /// </summary>
        public void Resolve()
        {
            for (int i = 0; i < game.Partie.Mission.NbTours; i++)
            {
                ResolveTurn(i);
            }
        }

        /// <summary>
        /// Résout un tour
        /// </summary>
        /// <param name="numTour"></param>
        private void ResolveTurn(int numTour)
        {
            int indicePremierJoueur = game.Partie.Joueurs.FirstIndexOf(j => j.IsCapitaine);
            if (indicePremierJoueur == -1)
            {
                return;
            }

            for (int i = indicePremierJoueur; i != indicePremierJoueur; i = (i + 1) % game.Partie.Joueurs.Count)
            {
                if (game.Partie.Joueurs[i].Actions[numTour] != null)
                {
                    ResolveAction(game.Partie.Joueurs[i], numTour);
                }
            }
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
        /// Résout une action c
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
                    break;
                case CAction.HUBLOT:
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
            InGameMenace zoneMenace = game.Partie.MenacesExternes[source.Zone].OrderByDescending(m => m.Position).ThenBy(m => m.TourArrive).First();

            // S'il y a une menace dans la zone on lui inflige des dégâts
            if (zoneMenace != null)
            {
                int degats = Math.Max(0, source.Canon.Power - (zoneMenace.Menace.Shield - zoneMenace.DegatsSubis));
                zoneMenace.DegatsSubis += degats;
                zoneMenace.Menace.CurrentHp = Math.Max(0, zoneMenace.Menace.CurrentHp - degats);
                if (zoneMenace.Menace.CurrentHp == 0)
                {
                    game.Partie.MenacesExternes[source.Zone].Remove(zoneMenace);
                    game.MenacesDetruites.Add(zoneMenace.Menace);
                }
            }

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
    }
}
