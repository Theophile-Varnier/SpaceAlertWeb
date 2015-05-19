using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Web;

namespace SpaceAlert.Web.Helpers
{
    /// <summary>
    /// Classe d'extension des url
    /// </summary>
    public static class UriExtensions
    {
        /// <summary>
        /// Méthode permettant de récupérer la valeur d'un paramètre dans une uri
        /// </summary>
        /// <param name="uri">L'uri</param>
        /// <param name="param">Le nom du paramètre à récupérer</param>
        /// <returns></returns>
        public static string GetParameter(this Uri uri, string param)
        {
            Regex regex = new Regex(string.Concat(param, "=([^&]+)"));
            Match match = regex.Match(uri.Query);
            return match.Success ? match.Groups[1].Value : null;
        }
    }
}