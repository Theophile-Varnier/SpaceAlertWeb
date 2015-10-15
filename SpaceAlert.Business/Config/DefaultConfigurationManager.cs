using SpaceAlert.Model.Jeu;
using System;

namespace SpaceAlert.Business.Config
{
    public class DefaultConfigurationManager : IGameInitializer
    {
        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <returns></returns>
        public GameConfig GetConfig(GameContext game)
        {
            return SpaceAlertData.GetObject<GameConfig>(game.Game.TypeMission.ToString());
        }

        /// <summary>
        /// Initializes the mission.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public Mission InitMission()
        {
            throw new NotImplementedException();
        }
    }
}
