using SpaceAlert.Model.Helpers.Enums;
using SpaceAlert.Model.Jeu;
using SpaceAlert.Model.Stats;

namespace SpaceAlert.Business.Config
{
    public interface IGameInitializer
    {
        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <returns></returns>
        GameContext InitGame(TypeMission typeMission, int nbJoueurs, bool blanches, bool jaunes, bool rouges, Personnage captain);

        /// <summary>
        /// Initializes the mission.
        /// </summary>
        /// <returns></returns>
        Mission InitMission();
    }
}
