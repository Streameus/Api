using System.Net;

namespace Streameus.Exceptions.HttpErrors
{
    /// <summary>
    /// Exception thrown in case of internal error
    /// </summary>
    public class ServerErrorException : ApiException
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public ServerErrorException()
            : base(HttpStatusCode.InternalServerError)
        {
        }

        /// <summary>
        /// Constructor with error message
        /// </summary>
        /// <param name="reason"></param>
        public ServerErrorException(string reason)
            : base(HttpStatusCode.InternalServerError, reason)
        {
        }
    }
}