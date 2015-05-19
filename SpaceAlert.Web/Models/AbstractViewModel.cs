using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpaceAlert.Web.Models
{
    public abstract class AbstractViewModel
    {
        public List<string> ErrorMessages { get; set; }

        public abstract bool IsValid();
    }
}