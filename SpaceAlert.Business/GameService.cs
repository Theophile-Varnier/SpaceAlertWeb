using SpaceAlert.Business.Exceptions;
using SpaceAlert.Business.Factories;
using SpaceAlert.DataAccess;
using SpaceAlert.Model.Helpers;
using SpaceAlert.Model.Helpers.Enums;
using SpaceAlert.Model.Jeu;
using SpaceAlert.Model.Jeu.Evenements;
using SpaceAlert.Model.Menaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using SpaceAlert.Model.Site;
using SpaceAlert.Model.Stats;

namespace SpaceAlert.Business
{
    public class GameService : AbstractService
    {

        public GameService(UnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }


        /// <summary>
        /// Initializes the game.
        /// </summary>
        /// <param name="typeMission">The type mission.</param>
        /// <param name="nbJoueurs">The nb joueurs.</param>
        /// <param name="blanches">if set to <c>true</c> [blanches].</param>
        /// <param name="jaunes">if set to <c>true</c> [jaunes].</param>
        /// <param name="rouges">if set to <c>true</c> [rouges].</param>
        /// <param name="captain">The captain.</param>
        /// <returns></returns>
        /// <exception cref="UserAlreadyInGameException"></exception>
        public int InitialiserGame(TypeMission typeMission, int nbJoueurs, bool blanches, bool jaunes, bool rouges, KeyValuePair<long, string> captain)
        {
            Membre membre = unitOfWork.MembreProvider.GetUniqueResult(m => m.Id == captain.Key);

            if (membre.CurrentGame != null)
            {
                throw new UserAlreadyInGameException();
            }

            GameContext res = GameFactory.CreateGame(typeMission, nbJoueurs, blanches, jaunes, rouges, unitOfWork.PersonnageProvider.Get(captain.Key, captain.Value));

            res.Game.Joueurs[0].Couleur = GetNextColor(res, res.Game.Joueurs[0].Personnage.Nom);
            //membre.CurrentGame = res.Id;
            InitialiserRampes(res);
            unitOfWork.GameContextProvider.Add(res);

            unitOfWork.Context.SaveChanges();

            return res.Id;
        }

        /// <summary>
        /// Initializes the mission.
        /// </summary>
        /// <param name="game">The game.</param>
        private void InitialiserMission(GameContext game)
        {
            Dictionary<string, Mission> allMissions = SpaceAlertData.GetAll<Mission>();

            KeyValuePair<string, Mission> val = allMissions.Where(m => m.Value.TypeMission == game.Game.TypeMission).GetNextRandom();
            game.Game.MissionId = val.Key;
            game.Game.Mission = val.Value;
            Dictionary<string, Menace> availableMenaces = SpaceAlertData.GetAll<Menace>().Where(kvp => game.Game.Difficulte.HasFlag(kvp.Value.Couleur)).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            foreach (EvenementMenace evenement in game.Game.Mission.Evenements.OfType<EvenementMenace>())
            {
                KeyValuePair<string, Menace> selectedMenace = availableMenaces.GetNextRandom(kvp => kvp.Value.Type == evenement.Type);
                availableMenaces.Remove(selectedMenace.Key);
                evenement.MenaceName = selectedMenace.Key;
                game.Game.MenacesExternes.Add(MenaceFactory.CreateMenace(game, evenement));
            }
        }

        /// <summary>
        /// Démarre une partie
        /// </summary>
        /// <param name="gameId"></param>
        public GameContext DemarrerGame(int gameId)
        {
            return DemarrerGame(unitOfWork.Context.GameContext
                .Include(g => g.Game)
                .Include(g => g.Game.Joueurs)
                .Include(g => g.Game.Joueurs.Select(j => j.Actions))
                .Include(g => g.Game.Joueurs.Select(j => j.Deck))
                .Include(g => g.Rampes)
                .Include(g => g.Game.MenacesExternes)
                .SingleOrDefault(g => g.Id == gameId));
        }

        /// <summary>
        /// Démarrer une partie
        /// </summary>
        /// <param name="game"></param>
        /// <returns>L'id de la partie qui a été commencée</returns>
        public GameContext DemarrerGame(GameContext game)
        {
            InitialiserMission(game);
            game.Statut = StatutPartie.Jeu;
            game.TourEnCours = 1;

            foreach (Joueur joueur in game.Game.Joueurs)
            {
                for (int i = 1; i <= game.Game.Mission.NbTours; i++)
                {
                    joueur.Actions.Add(new ActionInTour
                    {
                        Tour = i,
                        JoueurId = joueur.Id,
                        Joueur = joueur,
                        Action = null
                    });
                }
                for (int j = 0; j < 3; j++)
                {
                    Tuple<TypeAction, Direction> nextCarte = GetNextCard(game.Id);
                    joueur.Deck.Single(d => d.Mouvement == nextCarte.Item2 && d.TypeAction == nextCarte.Item1).NbCartes++;
                }
            }
            RegisterGame(game.Game);
            unitOfWork.Context.SaveChanges();
            return game;
        }

        /// <summary>
        /// Ajoute les rampes à une partie
        /// </summary>
        private void InitialiserRampes(GameContext game)
        {
            List<Rampe> allRampes = SpaceAlertData.GetAll<Rampe>().Select(kvp => kvp.Value).ToList();
            foreach (Zone zone in Enum.GetValues(typeof(Zone)))
            {
                game.Rampes.Add(new RampeInZone
                {
                    Zone = zone,
                    RampeId = allRampes.GetNextRandom(true).Id
                });
            }
            game.RampeInterneId = allRampes.GetNextRandom(true).Id;
        }

        /// <summary>
        /// Récupère toutes les parties en attente de joueurs
        /// </summary>
        /// <returns></returns>
        public List<Game> RecupererGameEnAttente()
        {
            IEnumerable<GameContext> contexts = unitOfWork.Context.GameContext
                .Include(g => g.Game)
                .Include(g => g.Game.Joueurs)
                .Where(g => g.Statut == StatutPartie.Creation && g.Game.Joueurs.Count < g.Game.NbJoueurs).ToList();
            return contexts.Select(g => g.Game).ToList();
        }

        /// <summary>
        /// Récupère une game enregistrée
        /// </summary>
        /// <param name="gameId"></param>
        /// <returns></returns>
        public GameContext GetGame(int gameId)
        {
            return unitOfWork.Context.GameContext
                .Include(g => g.Game)
                .Include(g => g.Game.Joueurs)
                .Include(g => g.Game.Vaisseau.Zones)
                .Include(g => g.Game.Joueurs.Select(j => j.Deck))
                .Include(g => g.Game.Joueurs.Select(j => j.Personnage))
                .Include(g => g.Game.Joueurs.Select(j => j.Personnage.Membre))
                .SingleOrDefault(g => g.Id == gameId);
        }

        /// <summary>
        /// Gets the game for execution.
        /// </summary>
        /// <param name="gameId">The game identifier.</param>
        /// <returns></returns>
        public GameContext GetGameForExecution(int gameId)
        {
            GameContext res = unitOfWork.Context.GameContext
                .Include(g => g.Game)
                .Include(g => g.Game.Joueurs)
                .Include(g => g.Game.MenacesExternes)
                .Include(g => g.Game.MenacesExternes.Select(m => m.Menace))
                .SingleOrDefault(g => g.Id == gameId);

            res.Game.Mission = SpaceAlertData.GetObject<Mission>(res.Game.MissionId);
            return res;
        }

        /// <summary>
        /// Gets the color of the player.
        /// </summary>
        /// <param name="gameId">The game identifier.</param>
        /// <param name="membreId">The membre identifier.</param>
        /// <returns></returns>
        public string GetPlayerColor(int gameId, long membreId)
        {
            // On récupère la partie en cours
            Game game = unitOfWork.Context.Games
                .Include(g => g.Joueurs)
                .Include(g => g.Joueurs.Select(j => j.Personnage))
                .SingleOrDefault(g => g.Id == gameId);

            Joueur joueur = game != null ? game.Joueurs.FirstOrDefault(j => j.Personnage.MembreId == membreId) : null;
            return joueur != null ? joueur.Couleur : null;
        }

        /// <summary>
        /// Gets the color of the next.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <param name="characterName">Name of the character.</param>
        /// <returns></returns>
        public string GetNextColor(GameContext game, string characterName)
        {
            int index = SpaceAlertData.PlayerColors.IndexOf(game.Game.Joueurs.First(j => j.Personnage.Nom == characterName).Couleur);
            int current = index;
            string color;
            do
            {
                current = (current + 1) % SpaceAlertData.PlayerColors.Count;
                color = SpaceAlertData.PlayerColors[current];
            } while (current != index && game.Game.Joueurs.Select(j => j.Couleur).Contains(color));

            game.Game.Joueurs.First(j => j.Personnage.Nom == characterName).Couleur = color;

            unitOfWork.Context.SaveChanges();

            return color;
        }

        /// <summary>
        /// Gets the color of the next.
        /// </summary>
        /// <param name="gameId">The game identifier.</param>
        /// <param name="characterName">Name of the character.</param>
        /// <returns></returns>
        public string GetNextColor(int gameId, string characterName)
        {
            GameContext game = GetGame(gameId);
            return GetNextColor(game, characterName);
        }

        /// <summary>
        /// Ajoute un joueur à une partie
        /// </summary>
        /// <param name="gameId">La partie concernée</param>
        /// <param name="memberId">L'id du membre qui rejoint la partie</param>
        /// <param name="characterName">Le nom du personnage du membre</param>
        public GameContext AjouterJoueur(int gameId, long memberId, string characterName)
        {
            Membre membre = unitOfWork.MembreProvider.GetUniqueResult(j => j.Id == memberId);

            GameContext game = GetGame(gameId);

            // On vérifie que la partie n'est pas pleine.
            if (game.Game.Joueurs.Count == game.Game.NbJoueurs)
            {
                throw new PartiePleineException();
            }

            // On vérifie que le membre n'est pas déjà dans une partie (eh oh!)
            if (membre.CurrentGame != null)
            {
                throw new UserAlreadyInGameException();
            }

            // membre.CurrentGame = gameId;

            // On ajoute le joueur à la partie
            game.Game.Joueurs.Add(JoueurFactory.CreateJoueur(unitOfWork.PersonnageProvider.Get(memberId, characterName), false, game.Game));

            // On lui assigne une couleur
            GetNextColor(gameId, characterName);

            unitOfWork.Context.SaveChanges();

            return game;
        }

        /// <summary>
        /// Gets the player character.
        /// </summary>
        /// <param name="gameId">The game identifier.</param>
        /// <param name="membreId">The membre identifier.</param>
        /// <returns></returns>
        public Personnage GetPlayerCharacter(int gameId, long membreId)
        {
            return GetGame(gameId).Game.Joueurs.Select(j => j.Personnage).SingleOrDefault(p => p.MembreId == membreId);
        }
         
        /// <summary>
        /// Enregistre une partie lorsqu'elle est lancée
        /// </summary>
        private void RegisterGame(Game game)
        {
            foreach (Joueur joueur in game.Joueurs)
            {
                unitOfWork.JoueurProvider.RegisterGame(joueur, game);
            }
        }

        /// <summary>
        /// Adds the player actions.
        /// </summary>
        /// <param name="membreId">The membre identifier.</param>
        /// <param name="gameId">The game identifier.</param>
        /// <param name="actions">The actions.</param>
        public void AddPlayerActions(long membreId, int gameId, IEnumerable<ActionInTour> actions)
        {
            Game game = unitOfWork.Context.Games
                .Include(g => g.Joueurs)
                .Include(g => g.Joueurs.Select(j => j.Personnage))
                .Include(g => g.Joueurs.Select(j => j.Actions))
                .SingleOrDefault(g => g.Id == gameId);
            game.Joueurs.Single(j => j.Personnage.MembreId == membreId).Actions.AddRange(actions);
            unitOfWork.Context.SaveChanges();
        }

        /// <summary>
        /// Adds the player action.
        /// </summary>
        /// <param name="membreId">The membre identifier.</param>
        /// <param name="gameId">The game identifier.</param>
        /// <param name="tour">The tour.</param>
        /// <param name="genre">The genre.</param>
        /// <param name="value">The value.</param>
        public void AddPlayerAction(long membreId, int gameId, int tour, GenreAction genre, int value)
        {
            unitOfWork.Context.Games.Find(gameId).Joueurs.Single(j => j.Personnage.MembreId == membreId).Actions.Add(new ActionInTour
            {
                Action = new ActionJoueur
                {
                    GenreAction = genre,
                    Value = value
                },
                Tour = tour
            });
            unitOfWork.Context.SaveChanges();
        }

        /// <summary>
        /// Gets the next card.
        /// </summary>
        /// <param name="gameId">The game identifier.</param>
        /// <returns></returns>
        public Tuple<TypeAction, Direction> GetNextCard(int gameId)
        {
            GameContext game = unitOfWork.Context.GameContext.Include(g => g.Deck).Single(g => g.Id == gameId);
            int nbCartes = game.Deck.Sum(deck => deck.NbCartes);
            Random rand = new Random();
            int val = rand.Next() % nbCartes;
            int lastCount = 0;
            foreach (PartialDeck deck in game.Deck.OrderByDescending(d => d.NbCartes))
            {
                lastCount += deck.NbCartes;
                if (deck.NbCartes > 0 && val < lastCount)
                {
                    deck.NbCartes--;
                    return new Tuple<TypeAction, Direction>(deck.TypeAction, deck.Mouvement);
                }
            }
            return null;
        }

        /// <summary>
        /// Resolves the game.
        /// </summary>
        /// <param name="gameId">The game identifier.</param>
        public void ResolveGame(int gameId)
        {
            GameContext game = ReconstructGame(gameId);

            GameResolutionManager manager = new GameResolutionManager(game);
            manager.Resolve();
            unitOfWork.Context.SaveChanges();

            game = EndGame(gameId);

            if (game.Game.Win)
            {
                UpdateTeamScore(gameId);
            }
        }

        /// <summary>
        /// Updates the team score.
        /// </summary>
        /// <param name="gameId">The game identifier.</param>
        private void UpdateTeamScore(int gameId)
        {
            int pts = ComputeGameScore(gameId);
            IEnumerable<Personnage> personnages = unitOfWork.JoueurProvider.GetGameCharacters(gameId);
            foreach (Personnage personnage in personnages)
            {
                unitOfWork.PersonnageProvider.AddXpPoints(personnage.Id, pts);
            }
        }

        /// <summary>
        /// Computes the game score.
        /// </summary>
        /// <param name="gameId">The game identifier.</param>
        public int ComputeGameScore(int gameId)
        {
            // TODO : ajouter les robots cassés, les joueurs assommés / dans les intercepteurs
            GameContext game = unitOfWork.Context.GameContext
                .Include(g => g.Game.MenacesExternes)
                .Include(g => g.Game.Vaisseau)
                .Include(g => g.Game.Joueurs)
                .Include(g => g.Game.Joueurs.Select(j => j.Statistiques))
                .SingleOrDefault(g => g.Id == gameId);

            int pointsDeVictoire = 0;

            IEnumerable<InGameMenace> menaces = game.Game.MenacesExternes; // .Union(game.Game.MenacesInternes)

            // On ajoute les points des menaces
            foreach (InGameMenace menace in menaces)
            {
                if (menace.Status == MenaceStatus.Detruite)
                {
                    pointsDeVictoire += SpaceAlertData.GetObject<Menace>(menace.MenaceName).PointsSiDetruite;
                }
                else
                {
                    pointsDeVictoire += SpaceAlertData.GetObject<Menace>(menace.MenaceName).PointsSiSurvecu;
                }
            }

            // On ajoute les points de hublot
            var pointsDeHublot = game.Game.Joueurs.Select(j => j.Statistiques).SelectMany(s => s.PointsDeHublot);
            foreach (int phase in pointsDeHublot.Select(h => h.Phase).Distinct())
            {
                pointsDeVictoire += pointsDeHublot.Where(p => p.Phase == phase).Max(p => p.NbPoints);
            }

            // On retire les points dûs aux dégâts sur les zones
            foreach (InGameZone zone in game.Game.Vaisseau.Zones)
            {
                pointsDeVictoire -= zone.Degats;
            }
            pointsDeVictoire -= game.Game.Vaisseau.Zones.Max(z => z.Degats);

            // On retire les points des joueurs assommés
            pointsDeVictoire -= 2 * game.Game.Joueurs.Count(j => j.Status == StatusJoueur.Assomme);

            // On retire les points des robots désactivés
            pointsDeVictoire -= game.Game.Joueurs.Count(j => j.Robots == EtatRobots.Casse);

            return pointsDeVictoire;
        }

        /// <summary>
        /// Ends the game.
        /// </summary>
        /// <param name="gameId">The game identifier.</param>
        public GameContext EndGame(int gameId)
        {
            GameContext game = GetGame(gameId);

            game.Game.Win = game.Game.Vaisseau.Zones.All(z => z.Degats < 6);

            game.Statut = StatutPartie.Termine;

            foreach (Membre membre in game.Game.Joueurs.Select(j => j.Personnage).Select(p => p.Membre))
            {
                membre.CurrentGame = null;
            }

            unitOfWork.Context.SaveChanges();

            return game;
        }

        /// <summary>
        /// Reconstructs the game.
        /// </summary>
        /// <param name="gameId">The game identifier.</param>
        /// <returns></returns>
        public GameContext ReconstructGame(int gameId)
        {
            GameContext res = unitOfWork.GameContextProvider.GetUniqueResult(g => g.Id == gameId, 
                g => g.Game.Joueurs,
                g => g.Game.Vaisseau.Zones,
                g => g.Game.Joueurs.Select(j => j.Actions.Select(a => a.Action)), 
                g => g.Game.MenacesExternes);

            res.Game.Mission = SpaceAlertData.GetObject<Mission>(res.Game.MissionId);

            foreach (InGameMenace menace in res.Game.MenacesExternes)
            {
                menace.Menace = SpaceAlertData.GetObject<Menace>(menace.MenaceName);
            }

            return res;
        }
    }
}
