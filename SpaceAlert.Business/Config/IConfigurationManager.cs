using SpaceAlert.Model.Jeu;

namespace SpaceAlert.Business.Config
{
    public interface IConfigurationManager
    {
        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <returns></returns>
        GameConfig GetConfig(GameContext game);

        /// <summary>
        /// Initializes the mission.
        /// </summary>
        /// <returns></returns>
        void InitMission(GameContext game);
    }
}
