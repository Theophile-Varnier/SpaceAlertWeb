using SpaceAlert.Model.Jeu;
using SpaceAlert.Model.Site;

namespace SpaceAlert.Web.Models.Mapping
{
    public static class AccountMapper
    {
        public static Membre MapFromViewModel(AccountViewModel model)
        {
            return new Membre
            {
                Pseudo = model.Pseudo,
                Email = model.Email,
                ClearPassWord = model.MotDePasse
            };
        }

        public static PlayerViewModel MapFromDto(Joueur joueur)
        {
            return new PlayerViewModel
            {
                Name = joueur.Personnage.Nom,
                MembreName = joueur.Personnage.Membre.Pseudo,
                Color = joueur.Couleur,
                Avatar = joueur.Personnage.Membre.Avatar
            };
        }
    }
}