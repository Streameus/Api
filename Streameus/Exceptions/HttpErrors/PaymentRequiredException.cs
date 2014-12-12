using System.Net;

namespace Streameus.Exceptions.HttpErrors
{
    /// <summary>
    /// Exception thrown when there is a payment issue
    /// </summary>
    public class PaymentRequiredException : ApiException
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reason">Constructor with error message</param>
        public PaymentRequiredException(string reason) : base(HttpStatusCode.PaymentRequired, reason)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public PaymentRequiredException()
            : base(HttpStatusCode.PaymentRequired)
        {
        }
    }
}