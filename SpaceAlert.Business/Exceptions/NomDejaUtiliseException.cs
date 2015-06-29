using System;

namespace SpaceAlert.Business.Exceptions
{
    public class NomDejaUtiliseException: Exception
    {
        public NomDejaUtiliseException(): base()
        {
            
        }

        public NomDejaUtiliseException(string message) : base(message)
        {
            
        }

        public NomDejaUtiliseException(string message, Exception innerException) : base(message, innerException)
        {
            
        }
    }
}
