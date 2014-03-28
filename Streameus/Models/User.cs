using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Streameus.Models
{
    public partial class User
    {
        /// <summary>
        /// Returns the full name, taking the visibility in account
        /// </summary>
        public string FullName
        {
            get
            {
                return (this.FirstNameVisibility ? this.FirstName : "") + " " +
                       (this.LastNameVisibility ? this.LastName : "");
            }
        }
    }
}