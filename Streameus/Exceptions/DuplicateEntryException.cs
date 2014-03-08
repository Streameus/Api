using System;

namespace Streameus.Exceptions
{
    /// <summary>
    /// Exception thrown when duplicates are found
    /// </summary>
    public class DuplicateEntryException : Exception
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="message">specific error message</param>
        public DuplicateEntryException(string message)
            : base(message)
        {
        }
    }
}