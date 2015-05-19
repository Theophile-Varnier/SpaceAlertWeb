using Spring.Context;
using Spring.Context.Support;

namespace SpaceAlert.Business
{
    /// <summary>
    /// Contient des informations sur le jeu
    /// </summary>
    public static class SpaceAlertStaticData
    {
        public static IApplicationContext AppContext { get; set; }

        /// <summary>
        /// Initialisation des constantes du jeu
        /// </summary>
        public static void Init()
        {
            AppContext = ContextRegistry.GetContext();
        }

    }
}