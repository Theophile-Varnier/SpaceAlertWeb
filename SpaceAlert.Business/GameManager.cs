using SpaceAlert.Model.Jeu;

namespace SpaceAlert.Business
{
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
                ResoudreTour(i);
            }
        }

        private void ResoudreTour(int numTour)
        {
            
        }
    }
}
