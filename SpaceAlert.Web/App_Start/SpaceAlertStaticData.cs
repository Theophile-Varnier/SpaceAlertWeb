using SpaceAlert.Model.Menaces;
using SpaceAlert.Model.Plateau;
using Spring.Context;
using Spring.Context.Support;

namespace SpaceAlert.Web
{
    /// <summary>
    /// Contient des informations sur le jeu
    /// </summary>
    public static class SpaceAlertStaticData
    {
        public static ListOfMenaces MenacesBlanches { get; set; }

        public static ListOfMenaces MenacesJaunes { get; set; }

        public static ListOfMenaces MenacesRouges { get; set; }

        public static Vaisseau DefaultVaisseau { get; set; }

        /// <summary>
        /// Initialisation des constantes du jeu
        /// </summary>
        public static void Init()
        {
            IApplicationContext ctx = ContextRegistry.GetContext();
            DefaultVaisseau = (Vaisseau)ctx.GetObject("Vaisseau");
            MenacesBlanches = (ListOfMenaces)ctx.GetObject("MenacesBlanches");
        }
    }
}