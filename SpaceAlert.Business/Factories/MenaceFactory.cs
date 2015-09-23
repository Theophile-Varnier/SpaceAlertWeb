using SpaceAlert.Model.Helpers.Enums;
using SpaceAlert.Model.Jeu;
using SpaceAlert.Model.Jeu.Evenements;
using SpaceAlert.Model.Menaces;
using System.Linq;

namespace SpaceAlert.Business.Factories
{
    public class MenaceFactory
    {
        /// <summary>
        /// Génère une menace en fonction du contexte d'une partie
        /// </summary>
        /// <param name="game">Le contexte de la partie</param>
        /// <param name="source">L'événement à partir duquel est générée la menace</param>
        /// <returns></returns>
        public static MenaceInZone CreateMenace(GameContext game, EvenementMenace source)
        {
            Menace selectedMenace = SpaceAlertData.Menace(source.MenaceName);
            InGameMenace menace = new InGameMenace
            {
                MenaceName = source.MenaceName,
                Status = MenaceStatus.Attente,
                CurrentHp = selectedMenace.MaxHp,
                CurrentShield = selectedMenace.Shield,
                CurrentSpeed = selectedMenace.Speed,
                Position = 0,
                TourArrive = source.TourArrive,
                DegatsSubis = 0
            };
            switch (source.Type)
            {
                case TypeMenace.MenaceExterne:
                case TypeMenace.MenaceExterneSerieuse:
                    menace.RampeId = game.Rampes.Single(r => r.Zone == source.Zone).Id;
                    break;
                case TypeMenace.MenaceInterne:
                case TypeMenace.MenaceInterneSerieuse:
                    menace.RampeId = game.RampeInterneId;
                    break;
                default:
                    // do the default action
                    break;
            }
            MenaceInZone res = new MenaceInZone
            {
                Menace = menace,
                GameId = game.Game.Id,
                Game = game.Game,
                Zone = source.Zone,
                AnnonceEvenement = source.Annonce
            };
            return res;
        }
    }
}
