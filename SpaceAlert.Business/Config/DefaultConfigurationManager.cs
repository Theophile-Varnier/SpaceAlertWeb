using SpaceAlert.Model.Jeu;
using SpaceAlert.Model.Jeu.Evenements;
using SpaceAlert.Model.Menaces;
using SpaceAlert.Model.Helpers;
using System;
using System.Linq;
using System.Collections.Generic;
using SpaceAlert.Business.Factories;

namespace SpaceAlert.Business.Config
{
    public class DefaultConfigurationManager : IConfigurationManager
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
        public void InitMission(GameContext game)
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
    }
}
