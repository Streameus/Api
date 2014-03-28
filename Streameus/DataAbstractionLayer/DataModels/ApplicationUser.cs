#pragma warning disable 1591
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Streameus.Models
{
    //This class are used to be able to use User as the auth class, they have no other purpose yet

    public class CustomRole : IdentityRole<int, CustomUserRole>
    {
        public CustomRole()
        {
        }

        public CustomRole(string name)
        {
            Name = name;
        }
    }

    public class CustomUserRole : IdentityUserRole<int>
    {
        public int Id { get; set; }
    }

    public class CustomUserClaim : IdentityUserClaim<int>
    {
    }

    public class CustomUserLogin : IdentityUserLogin<int>
    {
        public int Id { get; set; }
    }
}

#pragma warning restore 1591