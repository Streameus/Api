using System.Net;

namespace Streameus.Exceptions.HttpErrors
{
    /// <summary>
    /// Exception thrown when there is no results (204)
    /// </summary>
    public class NoResultException : ApiException
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public NoResultException()
            : base(HttpStatusCode.NoContent)
        {
        }

        /// <summary>
        /// Constructor with error message
        /// </summary>
        /// <param name="reason"></param>
        public NoResultException(string reason)
            : base(HttpStatusCode.NoContent, reason)
        {
        }
    }
}