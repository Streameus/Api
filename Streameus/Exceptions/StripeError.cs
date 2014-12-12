using System;

namespace Streameus.Exceptions
{
    /// <summary>
    /// Error thrown when stripe fails
    /// </summary>
    public class StreameusStripeError : Exception
    {
        /// <summary>
        /// Error code
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="message">specific error message</param>
        public StreameusStripeError(string message)
            : base(message)
        {
        }


        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="code">Error code</param>
        /// <param name="message">specific error message</param>
        public StreameusStripeError(int code, string message)
            : base(message)
        {
            this.Code = code;
        }
    }
}