using SpaceAlert.Model.Site;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace SpaceAlert.Web.Common
{
    public interface ICustomPrincipal : IPrincipal
    {
        long Id { get; set; }
    }
}