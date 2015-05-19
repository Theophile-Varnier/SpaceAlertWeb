using System.Web;

namespace SpaceAlert.Web.Helpers
{
    public static class SessionExtensions
    {
        /// <summary>
        /// Récupère une variable de session ou va la chercher dans les cookies
        /// </summary>
        /// <param name="session"></param>
        /// <param name="attribute"></param>
        /// <returns></returns>
        public static string Get(this HttpSessionStateBase session, string attribute)
        {
            // Si l'attribut est déjà présent dans la session on ne s'embête pas
            if (session[attribute] == null)
            {
                HttpCookie existingCookie = HttpContext.Current.Request.Cookies[attribute];

                // Sinon s'il est présent dans les cookies on le récupère
                if (existingCookie != null)
                {
                    session[attribute] = existingCookie.Value;
                }
            }
            return (string)session[attribute];
        }
    }
}