using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceAlert.Services.Exceptions
{
    public class MembreNonExistantException : Exception
    {
        public MembreNonExistantException()
        {
        }
        public MembreNonExistantException(string message)
            : base(message)
        {
        }
        public MembreNonExistantException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
