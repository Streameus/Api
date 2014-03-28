using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace Streameus.Results
{
    /// <summary>
    /// Class used to check if the user is well authenticated
    /// </summary>
    public class ChallengeResult : IHttpActionResult
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="loginProvider">the name of the provider</param>
        /// <param name="controller">the controller used</param>
        public ChallengeResult(string loginProvider, ApiController controller)
        {
            this.LoginProvider = loginProvider;
            this.Request = controller.Request;
        }

        /// <summary>
        /// Name of the provider
        /// </summary>
        public string LoginProvider { get; set; }

        /// <summary>
        /// the current request being processed
        /// </summary>
        public HttpRequestMessage Request { get; set; }

        /// <summary>
        /// Creates an <see cref="T:System.Net.Http.HttpResponseMessage"/> asynchronously.
        /// </summary>
        /// <returns>
        /// A task that, when completed, contains the <see cref="T:System.Net.Http.HttpResponseMessage"/>.
        /// </returns>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            this.Request.GetOwinContext().Authentication.Challenge(this.LoginProvider);

            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            response.RequestMessage = this.Request;
            return Task.FromResult(response);
        }
    }
}