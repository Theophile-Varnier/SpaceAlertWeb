using System;

namespace SpaceAlert.Business.Exceptions
{
    public class PartiePleineException : Exception
    {
        public PartiePleineException()
        {

        }

        public PartiePleineException(string message)
            : base(message)
        {

        }

        public PartiePleineException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
