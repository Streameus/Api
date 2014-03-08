using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Streameus.Exceptions;

namespace Streameus.Models
{
    /// <summary>
    /// An user
    /// </summary>
    public partial class User
    {
        /// <summary>
        /// Return an user full name
        /// </summary>
        public string FullName
        {
            get { return this.FirstName + " " + this.LastName; }
        }
    }
}