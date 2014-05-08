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
    public class NewMessageGroupViewModel
    {
        /// <summary>
        /// Message group
        /// </summary>
        public MessageGroupViewModel MessageGroup { get; set; }

        /// <summary>
        /// Messages
        /// </summary>
        public IEnumerable<MessageViewModel> Messages { get; set; }

        /// <summary>
        /// default constructor
        /// </summary>
        public NewMessageGroupViewModel()
        {
        }

        /// <summary>
        /// Instantiate VM with a newMessageGroup's values
        /// </summary>
        /// <param name="messageGroup">Message Group</param>
        /// <param name="userId">User Id</param>
        public NewMessageGroupViewModel(MessageGroup messageGroup, int userId = -1, int count = -1)
        {
            this.MessageGroup = new MessageGroupViewModel(messageGroup, userId, count);
            this.Messages = messageGroup.Messages.Select(message => new MessageViewModel(message)).ToList();
        }
    }
}