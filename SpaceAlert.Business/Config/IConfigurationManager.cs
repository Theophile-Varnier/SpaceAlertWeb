using SpaceAlert.Model.Jeu;

namespace SpaceAlert.Business.Config
{
    public interface IGameInitializer
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
        Mission InitMission();
    }
}
