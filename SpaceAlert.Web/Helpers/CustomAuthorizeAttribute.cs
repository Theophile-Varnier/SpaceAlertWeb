using System.Web;
using System.Web.Mvc;

namespace SpaceAlert.Web.Helpers
{
    /// <summary>
    /// Attribut d'authentification
    /// </summary>
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// Vérifie que l'utilisateur est bien authentifié
        /// /!\ Pas du tout sécure, mais osef un peu
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return httpContext.Session != null && httpContext.Session.Get("pseudo") != null;
        }
    }
}