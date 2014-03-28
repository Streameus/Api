using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using Streameus.Enums;
using Streameus.Models;

namespace Streameus.ViewModels
{
    /// <summary>
    /// The paramters view model is returned to the client to map user's parameters
    /// </summary>
    public class ParametersViewModel
    {
        /// <summary>
        /// NotifMail, to know if a user wants to recieve mails.
        /// </summary>
        public bool NotifMail { get; set; }

        /// <summary>
        /// Language
        /// </summary>
        public ModelEnums.Language Language { get; set; }

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
            this.NotifMail = user.Parameters.NotifMail;
            this.Language = (ModelEnums.Language) user.Language;
        }

        /// <summary>
        /// Set a user paramters settings from a <see cref="ParametersViewModel"/>
        /// </summary>
        /// <param name="user"></param>
        public void SetUserParameters(ref User user)
        {
            user.Parameters.NotifMail = this.NotifMail;
            user.Language = Convert.ToInt32(this.Language);
        }
    }
}