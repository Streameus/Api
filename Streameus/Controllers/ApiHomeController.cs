using System;
using System.Net.Http;
using Swashbuckle.Controllers;

namespace Streameus.Controllers
{
    /// <summary>
    /// The default controller, reached on /api
    /// </summary>
    public class ApiHomeController : BaseController
    {
        // GET api/api
        /// <summary>
        /// Return the default swagger api-docs view
        /// </summary>
        /// <returns>Json doc</returns>
        [Obsolete]
        public HttpResponseMessage Get()
        {
            var apiDocsController = new ApiDocsController();
            var reponse = apiDocsController.Index();
            return new HttpResponseMessage()
            {
                Content = new StringContent(
                    reponse.Content.Replace('\\', ' ')
                    ),
            };
        }
    }
}
