using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using Streameus.Models;

namespace Streameus.ViewModels
{
    /// <summary>
    /// The paramters view model is returned to the client to map user's parameters
    /// </summary>
    public class ParametersViewModel
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public ParametersViewModel()
        {
        }

        /// <summary>
        /// Instantiate VM with a user's values
        /// </summary>
        /// <param name="user"></param>
        public ParametersViewModel(User user)
        {
            this.NotifMail = user.Parameter.NotifMail;
            this.Language = user.Language;
        }

        /// <summary>
        /// Set a user paramters settings from a <see cref="ParametersViewModel"/>
        /// </summary>
        /// <param name="user"></param>
        public void SetUserParameters(ref User user)
        {
            user.Parameter.NotifMail = this.NotifMail;
            user.Language = this.Language;
        }

        /// <summary>
        /// NotifMail, to know if a user wants to recieve mails.
        /// </summary>
        public bool NotifMail { get; set; }

        /// <summary>
        /// Language
        /// </summary>
        public int Language { get; set; }
    }
}