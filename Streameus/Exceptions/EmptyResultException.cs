using System;

namespace Streameus.Exceptions
{
    /// <summary>
    /// Exception thrown when the result is an empty list or empty array
    /// </summary>
    public class EmptyResultException : Exception
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="message">Specific error message</param>
        public EmptyResultException(string message)
            : base(message)
        {
        }
    }
}