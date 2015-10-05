using SpaceAlert.Model.Jeu;
using System;
using System.Collections.Generic;

namespace SpaceAlert.Web.Models.Mapping
{
    public class ShipFactory
    {
        public static GameShipViewModel DefaultShip(Game game)
        {
            GameShipViewModel res = new GameShipViewModel
            {
                GameId = game.Id,
                MissionId = game.MissionId,
                PhaseEnCours = 1,
                Joueurs = new List<PlayerViewModel>(),
                CurrentMenace  = new CardViewModel
                {
                    
                },
                Salles = new List<SalleViewModel>
                {
                    new SalleViewModel
                    {
                        ContainersPos = new Dictionary<Tuple<int, int>, string>
                        {
                            {new Tuple<int, int>(0, 2), "red"},
                            {new Tuple<int, int>(0, 3), "blue"},
                            {new Tuple<int, int>(2, 2), "yellow"},
                            {new Tuple<int, int>(2, 3), "green"},
                            {new Tuple<int, int>(1, 4), "purple"},
                        },
                        ContainsChar = false
                    },
                    new SalleViewModel
                    {
                        ContainersPos = new Dictionary<Tuple<int, int>, string>
                        {
                            {new Tuple<int, int>(0, 1), "red"},
                            {new Tuple<int, int>(0, 2), "blue"},
                            {new Tuple<int, int>(0, 3), "yellow"},
                            {new Tuple<int, int>(1, 1), "green"},
                            {new Tuple<int, int>(1, 3), "purple"},
                        },
                        ContainsChar = true
                    },
                    new SalleViewModel
                    {
                        ContainersPos = new Dictionary<Tuple<int, int>, string>
                        {
                            {new Tuple<int, int>(1, 0), "red"},
                            {new Tuple<int, int>(1, 1), "blue"},
                            {new Tuple<int, int>(1, 2), "yellow"},
                            {new Tuple<int, int>(2, 1), "green"},
                            {new Tuple<int, int>(2, 2), "purple"},
                        },
                        ContainsChar = false
                    },
                    new SalleViewModel
                    {
                        ContainersPos = new Dictionary<Tuple<int, int>, string>
                        {
                            {new Tuple<int, int>(1, 0), "red"},
                            {new Tuple<int, int>(1, 1), "blue"},
                            {new Tuple<int, int>(1, 2), "yellow"},
                            {new Tuple<int, int>(2, 1), "green"},
                            {new Tuple<int, int>(2, 2), "purple"},
                        },
                        ContainsChar = false
                    },
                    new SalleViewModel
                    {
                        ContainersPos = new Dictionary<Tuple<int, int>, string>
                        {
                            {new Tuple<int, int>(0, 1), "red"},
                            {new Tuple<int, int>(0, 3), "blue"},
                            {new Tuple<int, int>(1, 1), "yellow"},
                            {new Tuple<int, int>(1, 2), "green"},
                            {new Tuple<int, int>(1, 3), "purple"},
                        },
                        ContainsChar = false
                    },
                    new SalleViewModel
                    {
                        ContainersPos = new Dictionary<Tuple<int, int>, string>
                        {
                            {new Tuple<int, int>(0, 4), "red"},
                            {new Tuple<int, int>(1, 1), "blue"},
                            {new Tuple<int, int>(0, 1), "yellow"},
                            {new Tuple<int, int>(1, 4), "green"},
                            {new Tuple<int, int>(2, 3), "purple"},
                        },
                        ContainsChar = false
                    },
                }
            };

            foreach (Joueur joueur in game.Joueurs)
            {
                res.Joueurs.Add(new PlayerViewModel
                {
                    Name = joueur.Personnage.Nom,
                    MembreName = joueur.Personnage.Membre.Pseudo,
                    Avatar = joueur.Personnage.Membre.Avatar,
                    Color = joueur.Couleur
                });
            }

            return res;
        }
    }
}