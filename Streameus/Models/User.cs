using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Streameus.Exceptions;

namespace Streameus.Models
{
    public partial class User
    {
        public string FullName
        {
            get { return this.FirstName + " " + this.LastName; }
        }
    }
}