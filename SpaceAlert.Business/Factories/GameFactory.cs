using SpaceAlert.Model.Helpers.Enums;
using SpaceAlert.Model.Jeu;
using SpaceAlert.Model.Menaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                Joueurs = new List<Joueur>(nbJoueurs)
            };

            // Ajoute les joueurs
            Joueur capitaine = JoueurFactory.CreateJoueur(captain.Key, captain.Value, true);
            game.Joueurs.Add(capitaine);

            // Initialise le contexte
            GameContext res = new GameContext
            {
                Statut = StatutPartie.CREATION,
                Partie = game,
                MenacesDisponibles = new ListOfMenaces()
            };

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
                GameService.ProchaineCouleur(game.Id, joueur.NomPersonnage);
            }
            return res;
        }
    }
}
