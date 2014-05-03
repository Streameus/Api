using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Streameus.Tests
{
    /// <summary>
    /// This class is used to mock the Identity used in the controllers
    /// </summary>
    public static class IdentityMocker
    {
        /// <summary>
        /// Set the fake Id to be returned by User.Identity.GetUserId
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="controller">The controller to be prepared</param>
        /// <param name="id">The id to be returned</param>
        public static void SetIdentityUserId<T>(ref T controller, int id) where T : ApiController
        {
            controller.User = new ClaimsPrincipal(
                new GenericPrincipal(new ClaimsIdentity(new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, id.ToString()),
                }), null));
        }
    }
}