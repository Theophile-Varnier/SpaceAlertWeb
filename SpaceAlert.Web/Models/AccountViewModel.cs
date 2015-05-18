using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpaceAlert.Web.Models
{
    public class AccountViewModel
    {
        public string Pseudo { get; set; }

        public string Email { get; set; }

        public string MotDePasse { get; set; }

        public string Confirmation { get; set; }
    }
}