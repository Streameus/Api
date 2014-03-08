using System.Net;

namespace Streameus.Exceptions.HttpErrors
{
    /// <summary>
    /// Exception thrown when the resource was not found (404)
    /// </summary>
    public class NotFoundException : ApiException
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public NotFoundException()
            : base(HttpStatusCode.NotFound)
        {
        }

        /// <summary>
        /// Constructor with error message
        /// </summary>
        /// <param name="reason"></param>
        public NotFoundException(string reason)
            : base(HttpStatusCode.NotFound, reason)
        {
        }
    }
}