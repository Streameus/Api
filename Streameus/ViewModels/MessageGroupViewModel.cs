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
        /// MessageGroup image id
        /// </summary>
        public int ImageId { get; set; }

        /// <summary>
        /// MessageGroup messages number
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// MessageGroup last message date
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        /// default constructor
        /// </summary>
        public MessageGroupViewModel()
        {
        }

        /// <summary>
        /// Instantiate VM with a messageGroup's values
        /// </summary>
        /// <param name="messageGroup">Message Group</param>
        /// <param name="userId">User Id</param>
        public MessageGroupViewModel(MessageGroup messageGroup, int userId = -1, int count = -1)
        {
            this.Id = messageGroup.Id;
            this.Count = count == -1 ? messageGroup.Messages.Count : count;
            if (userId < 0)
            {
                this.Members = messageGroup.Members.Select(member => member.Pseudo).ToArray();
                this.ImageId = messageGroup.Members.First().Id;
            }
            else
            {
                this.Members = messageGroup.Members.Where(i => i.Id != userId).Select(member => member.FullName).ToArray();
                this.ImageId = messageGroup.Members.First(i => i.Id != userId).Id;
            }
            if (messageGroup.Messages.Count > 0)
            {
                var lastMessage = messageGroup.Messages.OrderByDescending(m => m.Date).First();
                if (lastMessage.Content.Length >= 64)
                    this.Message = lastMessage.Content.Substring(0, 63) + "...";
                else
                    this.Message = lastMessage.Content;
                this.Date = lastMessage.Date.ToShortDateString() + " " + lastMessage.Date.ToShortTimeString();
            }
        }
    }
}