using System;
using System.Collections.Generic;
using SpaceAlert.Model.Helpers.Enums;
using SpaceAlert.Model.Jeu;

namespace SpaceAlert.Web.Models.Mapping
{
    public static class GameMapper
    {
        public static Game MapFromModel(GameViewModel model)
        {
            Game res = new Game
            {
                TypeMission = model.TypeMission,
                DateCreation = DateTime.Now
            };
            if (model.Blanches)
            {
                res.Difficulte |= Couleur.BLANCHE;
            }
            if (model.Jaunes)
            {
                res.Difficulte |= Couleur.JAUNE;
            }
            if (model.Rouges)
            {
                res.Difficulte |= Couleur.ROUGE;
            }

            res.Joueurs = new List<Joueur>();
            if (model.Players != null)
            {
                foreach (PlayerViewModel player in model.Players)
                {
                    res.Joueurs.Add(new Joueur { NomPersonnage = player.Name });
                }
            }
            return res;
        }

        public static GameViewModel MapToModel(Game source)
        {
            return new GameViewModel
            {
                DateCreation = source.DateCreation,
                TypeMission = source.TypeMission,
                Blanches = source.Difficulte.HasFlag(Couleur.BLANCHE),
                Jaunes = source.Difficulte.HasFlag(Couleur.JAUNE),
                Rouges = source.Difficulte.HasFlag(Couleur.ROUGE)
            };
        }
    }
}