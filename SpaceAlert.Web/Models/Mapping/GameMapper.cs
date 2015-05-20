using SpaceAlert.Model.Helpers.Enums;
using SpaceAlert.Model.Jeu;

namespace SpaceAlert.Web.Models.Mapping
{
    public static class GameMapper
    {
        public static Game MapFromModel(GameViewModel model)
        {
            Game res = new Game
            {
                TypeMission = model.TypeMission
            };
            if (model.Blanches)
            {
                res.Difficulte |= Couleur.BLANCHE;
            }
            if (model.Jaunes)
            {
                res.Difficulte |= Couleur.JAUNE;
            }
            if (model.Rouges)
            {
                res.Difficulte |= Couleur.ROUGE;
            }
            return res;
        }
    }
}