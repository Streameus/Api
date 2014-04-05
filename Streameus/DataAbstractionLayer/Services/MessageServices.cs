using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;
using Streameus.App_GlobalResources;
using Streameus.DataAbstractionLayer.Contracts;
using Streameus.DataBaseAccess;
using Streameus.Exceptions;
using Streameus.Models;

namespace Streameus.DataAbstractionLayer.Services
{
    /// <summary>
    /// Message services
    /// </summary>
    public class MessageServices : BaseServices<Message>, IMessageServices
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="unitOfWork"></param>
        public MessageServices(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        /// <summary>
        /// Save message
        /// </summary>
        /// <param name="message"></param>
        protected override void Save(Message message)
        {
            if (message.Id > 0)
                this.Update(message);
            else
                this.Insert(message);
            this.SaveChanges();
        }

        /// <summary>
        /// Add a new message in db
        /// </summary>
        /// <param name="newMessage">The message to be added</param>
        public void AddMessage(Message newMessage)
        {
            this.Save(newMessage);
        }

        /// <summary>
        /// Update an message
        /// </summary>
        /// <param name="message"></param>
        public void UpdateMessage(Message message)
        {
            this.Save(message);
        }

        /// <summary>
        /// Delete an message
        /// </summary>
        /// <param name="id">Id of the message to be deleted</param>
        public new void Delete(int id)
        {
            var messageToDelete = this.GetById(id);
            base.Delete(messageToDelete);
            this.SaveChanges();
        }

    }
}