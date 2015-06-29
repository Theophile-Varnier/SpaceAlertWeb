using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceAlert.Services.Exceptions
{
    public class MotDePasseInvalideException : Exception
    {
        public MotDePasseInvalideException()
        {
        }
        public MotDePasseInvalideException(string message)
            : base(message)
        {
        }
        public MotDePasseInvalideException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
