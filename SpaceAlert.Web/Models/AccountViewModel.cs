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
        public override bool IsValid()
        {
            // On vérifie que la confirmation du mot de passe est bonne
            return Confirmation == MotDePasse;
        }
    }
}