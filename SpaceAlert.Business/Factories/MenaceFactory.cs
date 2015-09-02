using SpaceAlert.Model.Helpers.Enums;
using SpaceAlert.Model.Jeu;
using SpaceAlert.Model.Jeu.Evenements;

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
        public static InGameMenace CreateMenace(GameContext game, EvenementMenace source)
        {
            InGameMenace res = new InGameMenace
            {
                Menace = source.Menace,
                Status = MenaceStatus.EnJeu,
                CurrentHp = source.Menace.MaxHp,
                CurrentShield = source.Menace.Shield,
                CurrentSpeed = source.Menace.Speed,
                Position = 0,
                TourArrive = source.TourArrive,
                DegatsSubis = 0
            };
            switch (source.Type)
            {
                case TypeMenace.MENACE_EXTERNE:
                case TypeMenace.MENACE_EXTERNE_SERIEUSE:
                    res.Rampe = game.Rampes[source.Zone];
                    break;
                case TypeMenace.MENACE_INTERNE:
                case TypeMenace.MENACE_INTERNE_SERIEUSE:
                    res.Rampe = game.RampeInterne;
                    break;
            }
            return res;
        }
    }
}
