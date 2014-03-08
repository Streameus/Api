using System.Net;

namespace Streameus.Exceptions.HttpErrors
{
    /// <summary>
    /// Exception thrown in case of conflict with ressource provided (409)
    /// </summary>
    public class ConflictdException : ApiException
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public ConflictdException()
            : base(HttpStatusCode.Conflict)
        {
        }

        /// <summary>
        /// Constructor with error message
        /// </summary>
        /// <param name="reason"></param>
        public ConflictdException(string reason)
            : base(HttpStatusCode.Conflict, reason)
        {
        }
    }
}