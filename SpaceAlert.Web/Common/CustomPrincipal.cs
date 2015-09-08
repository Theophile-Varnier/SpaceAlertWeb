using SpaceAlert.Model.Site;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace SpaceAlert.Web.Common
{
    public class CustomPrincipal : ICustomPrincipal
    {
        public long Id { get; set; }

        public IIdentity Identity { get; private set; }

        public bool IsInRole(string role)
        {
            return Identity != null && Identity.IsAuthenticated;
        }

        public CustomPrincipal(string userName)
        {
            Identity = new GenericIdentity(userName);
        }
    }
}