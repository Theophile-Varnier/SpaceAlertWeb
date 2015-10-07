using SpaceAlert.Model.Jeu;
using System;
using System.Linq;
using System.Collections.Generic;

namespace SpaceAlert.Web.Models.Mapping
{
    public class ShipFactory
    {
        public static GameShipViewModel DefaultShip(Game game, int charSelected)
        {
            GameShipViewModel res = new GameShipViewModel
            {
                GameId = game.Id,
                MissionId = game.MissionId,
                PhaseEnCours = 1,
                Joueurs = new List<PlayerViewModel>(),
                StartingDeck = new List<CardViewModel>(),
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
                    PersonnageId = joueur.IdPersonnage,
                    Avatar = joueur.Personnage.Membre.Avatar,
                    MembreName = joueur.Personnage.Membre.Pseudo,
                    Color = joueur.Couleur
                });
            }
            Joueur currentPlayer = game.Joueurs.Single(j => j.IdPersonnage == charSelected);

            foreach (PartialDeck deck in currentPlayer.Deck)
            {
                for (int i = 0; i < deck.NbCartes; i++)
                {
                    res.StartingDeck.Add(new CardViewModel
                    {
                        Type = (int)deck.TypeAction,
                        Direction = (int)deck.Mouvement
                    });
                }
            }

            res.ClientPlayer = res.Joueurs.Single(j => j.PersonnageId == charSelected);

            return res;
        }
    }
}