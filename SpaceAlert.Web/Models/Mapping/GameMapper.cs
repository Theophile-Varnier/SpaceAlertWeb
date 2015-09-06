using System;
using System.Collections.Generic;
using SpaceAlert.Model.Helpers.Enums;
using SpaceAlert.Model.Jeu;

namespace SpaceAlert.Web.Models.Mapping
{
    public static class GameMapper
    {

        public static GameViewModel MapToModel(Game source)
        {
            GameViewModel res = new GameViewModel
            {
                DateCreation = source.DateCreation,
                TypeMission = source.TypeMission,
                GameId = source.Id,
                Blanches = source.Difficulte.HasFlag(Couleur.BLANCHE),
                Jaunes = source.Difficulte.HasFlag(Couleur.JAUNE),
                NbJoueurs = source.Joueurs.Capacity,
                Players = new List<PlayerViewModel>(),
                Rouges = source.Difficulte.HasFlag(Couleur.ROUGE)
            };

            foreach (Joueur joueur in source.Joueurs)
            {
                res.Players.Add(new PlayerViewModel
                {
                    Name = joueur.Personnage.Nom,
                    Color = joueur.Couleur
                });
            }
            return res;
        }
    }
}