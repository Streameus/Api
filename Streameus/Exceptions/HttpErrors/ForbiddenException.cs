using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace Streameus.Exceptions.HttpErrors
{
    /// <summary>
    /// An exception to throw to get a 403
    /// </summary>
    public class ForbiddenException : ApiException
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public ForbiddenException() : base(HttpStatusCode.Forbidden)
        {
        }

        /// <summary>
        /// Constructor with error message
        /// </summary>
        /// <param name="reason"></param>
        public ForbiddenException(string reason) : base(HttpStatusCode.Forbidden, reason)
        {
        }
    }
}