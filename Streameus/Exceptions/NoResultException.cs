using System;

namespace Streameus.Exceptions
{
    public class NoResultException : Exception
    {
        public NoResultException(string message)
            : base(message)
        {
        }
    }
}