using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpaceAlert.Web.Hubs
{
    public class UserAlreadyInGameException : Exception
    {
        public UserAlreadyInGameException()
        {

        }

        public UserAlreadyInGameException(string message)
            : base(message)
        {

        }

        public UserAlreadyInGameException(string message, Exception innException)
            : base(message, innException)
        {

        }
    }
}