using SpaceAlert.Business.Factories;
using SpaceAlert.Model.Helpers.Enums;
using SpaceAlert.Model.Jeu;
using SpaceAlert.Model.Stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceAlert.Business.Config
{
    public class DefaultConfigurationManager : IGameInitializer
    {
        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <returns></returns>
        public GameContext InitGame(TypeMission typeMission, int nbJoueurs, bool blanches, bool jaunes, bool rouges, Personnage captain)
        {
            GameContext res = GameFactory.CreateGame(typeMission, nbJoueurs, blanches, jaunes, rouges, captain);
            return res;
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
