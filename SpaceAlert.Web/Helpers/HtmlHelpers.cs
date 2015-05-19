using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpaceAlert.Web.Helpers
{
    public static class HtmlHelpers
    {
        public static object GetHtmlAttribute(string cssClass, bool disable)
        {
            if (disable)
            {
                return new { @class = cssClass, @disabled = "" };
            }
            return new { @class = cssClass };
        }
    }
}