﻿using SpaceAlert.Model.Site;

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
    }
}