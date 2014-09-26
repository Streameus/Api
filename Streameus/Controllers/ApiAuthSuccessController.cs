using System;
using System.Net.Http;
using System.Web.Http;

namespace Streameus.Controllers
{
    /// <summary>
    /// The useless controller, reached on /
    /// </summary>
    public class ApiAuthSuccessController : BaseController
    {
        // GET api/api
        /// <summary>
        /// Url used to ensure that the auth worked
        /// </summary>
        /// <returns>String</returns>
        [Route("AuthSuccess")]
        [Obsolete]
        public String Get()
        {
            return "It's a me, Mario!";
        }
    }
}