using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Streameus.DataAbstractionLayer;

namespace Streameus.Models
{
    /// <summary>
    /// Streameus implementation of UserStore, to be able to use User and int as Primary Keys
    /// </summary>
    public class StreameusUserStore : UserStore<User, CustomRole, int, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="context"></param>
        public StreameusUserStore(StreameusContext context)
            : base(context)
        {
        }

        /// <summary>
        /// Overload to delete the CustomUserLogin from the db (prevents ForeignKeyError)
        /// </summary>
        /// <param name="user"></param>
        /// <param name="login"></param>
        /// <returns></returns>
        public override Task RemoveLoginAsync(User user, UserLoginInfo login)
        {
            var provider = login.LoginProvider;
            var key = login.ProviderKey;
            var entry = user.Logins.SingleOrDefault(l => l.LoginProvider == provider && l.ProviderKey == key);
            if (entry != null)
            {
                user.Logins.Remove(entry);
                this.Context.Set<CustomUserLogin>().Remove(entry);
            }
            return Task.FromResult(0);
        }
    }

    /// <summary>
    /// Role store for the app
    /// </summary>
    public class StreameusRoleStore : RoleStore<CustomRole, int, CustomUserRole>
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="context"></param>
        public StreameusRoleStore(StreameusContext context)
            : base(context)
        {
        }
    }

    /// <summary>
    /// Streameus UserManager, derived from base UserManager, but using Ints
    /// </summary>
    public class StreameusUserManager : UserManager<User, int>
    {
        /// <summary>
        /// Configure the application user manager
        /// </summary>
        /// <param name="store"></param>
        public StreameusUserManager(IUserStore<User, int> store) : base(store)
        {
        }

        /// <summary>
        /// Static factory method to be called by Owin to create the usermanager
        /// </summary>
        /// <param name="options"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public static StreameusUserManager Create(IdentityFactoryOptions<StreameusUserManager> options,
            IOwinContext context)
        {
            var manager = new StreameusUserManager(new StreameusUserStore(context.Get<StreameusContext>()));
            manager.UserValidator = new UserValidator<User, int>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };
            manager.PasswordValidator = new PasswordValidator()
            {
                RequiredLength = 6,
            };
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<User, int>(dataProtectionProvider.Create("PasswordReset"));
            }
            return manager;
        }
    }
}