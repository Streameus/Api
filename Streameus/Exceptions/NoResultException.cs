using System;

namespace Streameus.Exceptions
{
    /// <summary>
    /// Exception thrown when no results are found
    /// </summary>
    public class NoResultException : Exception
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="message">Specific error message</param>
        public NoResultException(string message)
            : base(message)
        {
        }
    }
}