using System.Net;

namespace Streameus.Exceptions.HttpErrors
{
    /// <summary>
    /// Exception thrown when the request is malformed (400)
    /// </summary>
    public class BadRequestException : ApiException
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public BadRequestException()
            : base(HttpStatusCode.BadRequest)
        {
        }

        /// <summary>
        /// Constructor with error message
        /// </summary>
        /// <param name="reason"></param>
        public BadRequestException(string reason)
            : base(HttpStatusCode.BadRequest, reason)
        {
        }
    }
}