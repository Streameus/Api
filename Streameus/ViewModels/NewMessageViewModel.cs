using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Streameus.Models;

namespace Streameus.ViewModels
{
    /// <summary>
    /// The message view model is returned to the client to map a message
    /// </summary>
    public class NewMessageViewModel
    {
        /// <summary>
        /// Message id
        /// </summary>
        public int MessageGroupId { get; set; }


        /// <summary>
        /// Id of the user who sent this message
        /// </summary>
        public IEnumerable<int> Recipients { get; set; }

        /// <summary>
        /// Message content
        /// </summary>
        public string Content { get; set; }

    }
}