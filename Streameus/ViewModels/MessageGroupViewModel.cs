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
        /// MessageGroup members
        /// </summary>
        public string[] Members { get; set; }

        /// <summary>
        /// MessageGroup last message content
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// MessageGroup messages number
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// MessageGroup last message date
        /// </summary>
        public DateTime Date { get; set; }

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
            this.Members = messageGroup.Members.Select(member => member.Pseudo).ToArray();
            var lastMessage = messageGroup.Messages.OrderByDescending(m => m.Date).First();
            if (lastMessage.Content.Length > 144)
                this.Message = lastMessage.Content.Substring(0, 144);
            else
                this.Message = lastMessage.Content;
            this.Date = lastMessage.Date;
            this.Count = messageGroup.Messages.Count;
        }
    }
}