using SpaceAlert.Model.Helpers.Enums;
using SpaceAlert.Model.Jeu;
using SpaceAlert.Model.Plateau;
using SpaceAlert.Model.Stats;
using System;
using System.Collections.Generic;

namespace SpaceAlert.Business.Factories
{
    public static class GameFactory
    {
        public static GameContext CreateGame(TypeMission typeMission, int nbJoueurs, bool blanches, bool jaunes, bool rouges, Personnage captain)
        {
            // Créé la partie
            Game game = new Game
            {
                TypeMission = typeMission,
                DateCreation = DateTime.Now,
                DateFin = DateTime.Now,
                Vaisseau = SpaceAlertData.GetObject<Vaisseau>("Vaisseau"),
                MenacesExternes = new List<MenaceInZone>(),
                NbJoueurs = nbJoueurs,
                Joueurs = new List<Joueur>()
            };

            // Initialise le contexte
            GameContext res = new GameContext
            {
                Statut = StatutPartie.CREATION,
                Game = game,
                Id = game.Id,
                Rampes = new List<RampeInZone>()
            };
            // Ajoute les joueurs
            Joueur capitaine = JoueurFactory.CreateJoueur(captain, true, game);
            game.Joueurs.Add(capitaine);

            // Ajout des menaces
            if (blanches)
            {
                game.Difficulte |= Couleur.BLANCHE;
            }
            if (jaunes)
            {
                game.Difficulte |= Couleur.JAUNE;
            }
            if (rouges)
            {
                game.Difficulte |= Couleur.ROUGE;
            }
            return res;
        }
    }
}
