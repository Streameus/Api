using System;
using System.Security.Claims;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Streameus.Models;

namespace Streameus.Controllers
{
    /// <summary>
    /// This is here only in case extensions are needed
    /// </summary>
    public class BaseController : ApiController
    {
        /// <summary>
        /// Get the current user Id, already parsed in int format
        /// </summary>
        /// <returns></returns>
        protected int GetCurrentUserId()
        {
            return Convert.ToInt32(this.User.Identity.GetUserId());
        }
    }
}