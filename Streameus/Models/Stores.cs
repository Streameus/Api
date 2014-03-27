using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace Streameus.Models
{
    public class StreameusUserStore : UserStore<User, CustomRole, int, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public StreameusUserStore(StreameusContext context)
            : base(context)
        {
        }
    }

    public class StreameusRoleStore : RoleStore<CustomRole, int, CustomUserRole>
    {
        public StreameusRoleStore(StreameusContext context)
            : base(context)
        {
        }
    }

    public class StreameusUserManager : UserManager<User, int>
    {
        // Configure the application user manager
        public StreameusUserManager(IUserStore<User, int> store) : base(store)
        {
        }

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
                RequireNonLetterOrDigit = true
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