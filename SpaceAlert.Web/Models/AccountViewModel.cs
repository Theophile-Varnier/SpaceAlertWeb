using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpaceAlert.Web.Models
{
    public class AccountViewModel : AbstractViewModel
    {
        public string Pseudo { get; set; }

        public string Email { get; set; }

        public string MotDePasse { get; set; }

        public string Confirmation { get; set; }

        /// <summary>
        /// Vérifie que le modèle est valide
        /// </summary>
        /// <returns></returns>
        public override bool Validate()
        {
            bool res = true;
            if (ErrorMessages != null)
            {
                ErrorMessages.Clear();
            }
            else
            {
                ErrorMessages = new List<string>();
            }
            if (string.IsNullOrWhiteSpace(Pseudo))
            {
                ErrorMessages.Add("Le pseudo ne peut pas être vide");
                res = false;
            }
            if (string.IsNullOrWhiteSpace(Email))
            {
                ErrorMessages.Add("L'email ne peut pas être vide");
            }

            if (string.IsNullOrWhiteSpace(MotDePasse))
            {
                ErrorMessages.Add("Le mot de passe ne peut pas être vide");
            }
            if (Confirmation != MotDePasse)
            {
                ErrorMessages.Add("Confirmation incorrecte.");
                // On vérifie que la confirmation du mot de passe est bonne
                res = false;
            }
            return res;
        }
    }
}