using SpaceAlert.Model.Jeu;

namespace SpaceAlert.Business.Config
{
    public interface IConfigurationManager
    {
        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <returns></returns>
        GameConfig GetConfig();

        /// <summary>
        /// Initializes the mission.
        /// </summary>
        /// <returns></returns>
        Mission InitMission();
    }
}
