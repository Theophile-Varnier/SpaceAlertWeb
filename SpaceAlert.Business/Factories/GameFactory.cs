using SpaceAlert.Model.Helpers.Enums;
using SpaceAlert.Model.Jeu;
using SpaceAlert.Model.Menaces;
using SpaceAlert.Model.Plateau;
using System;
using System.Collections.Generic;

namespace SpaceAlert.Business.Factories
{
    public class GameFactory
    {
        public static GameContext CreateGame(TypeMission typeMission, int nbJoueurs, bool blanches, bool jaunes, bool rouges, KeyValuePair<long, string> captain)
        {
            // Créé la partie
            Game game = new Game
            {
                TypeMission = typeMission,
                DateCreation = DateTime.Now,
                Vaisseau = SpaceAlertData.GetObject<Vaisseau>("Vaisseau"),
                MenacesExternes = new Dictionary<Zone,List<InGameMenace>>(),
                Joueurs = new List<Joueur>(nbJoueurs)
            };

            // Initialise le contexte
            GameContext res = new GameContext
            {
                Statut = StatutPartie.CREATION,
                Partie = game,
                MenacesDisponibles = new ListOfMenaces(),
                Rampes = new Dictionary<Zone,Rampe>()
            };
            // Ajoute les joueurs
            Joueur capitaine = JoueurFactory.CreateJoueur(captain.Key, captain.Value, true, game);
            game.Joueurs.Add(capitaine);

            // Ajout des menaces
            if (blanches)
            {
                game.Difficulte |= Couleur.BLANCHE;
                res.MenacesDisponibles += SpaceAlertData.GetObject<ListOfMenaces>("MenacesBlanches");
            }
            if (jaunes)
            {
                game.Difficulte |= Couleur.JAUNE;
                //res.MenacesDisponibles += SpaceAlertData.GetObject<ListOfMenaces>("MenacesJaunes");
            }
            if (rouges)
            {
                game.Difficulte |= Couleur.ROUGE;
                //res.MenacesDisponibles += SpaceAlertData.GetObject<ListOfMenaces>("MenacesRouges");
            }

            // Initialise les couleurs des joueurs
            foreach (Joueur joueur in game.Joueurs)
            {
                GameService.ProchaineCouleur(res, joueur.NomPersonnage);
            }
            return res;
        }
    }
}
