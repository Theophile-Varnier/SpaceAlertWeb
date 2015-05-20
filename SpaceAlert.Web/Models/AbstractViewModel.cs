using System.Collections.Generic;

namespace SpaceAlert.Web.Models
{
    public abstract class AbstractViewModel
    {
        public List<string> ErrorMessages { get; set; }

        public abstract bool Validate();
    }
}