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
    public class MessageGroupViewModel
    {
        /// <summary>
        /// MessageGroup id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// default constructor
        /// </summary>
        public MessageGroupViewModel()
        {
        }

        /// <summary>
        /// Instantiate VM with a messageGroup's values
        /// </summary>
        /// <param name="messageGroup"></param>
        public MessageGroupViewModel(MessageGroup messageGroup)
        {
            this.Id = messageGroup.Id;
        }
    }
}