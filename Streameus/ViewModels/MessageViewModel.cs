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
    public class MessageViewModel
    {
        /// <summary>
        /// Message id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Message content
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Date of this message
        /// </summary>
        public System.DateTime Date { get; set; }

        /// <summary>
        /// Id of the user who sent this message
        /// </summary>
        public int SenderId { get; set; }

        /// <summary>
        /// Id of the user who sent this message
        /// </summary>
        public int GroupId { get; set; }

        /// <summary>
        /// default constructor
        /// </summary>
        public MessageViewModel()
        {
        }

        /// <summary>
        /// Instantiate VM with a message's values
        /// </summary>
        /// <param name="message"></param>
        public MessageViewModel(Message message)
        {
            this.Id = message.Id;
            this.Date = message.Date;
            this.Content = message.Content;
            this.SenderId = message.SenderId;
            this.GroupId = message.GroupId;
        }
    }
}