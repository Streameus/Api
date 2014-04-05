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
    /// MessageGroup services
    /// </summary>
    public class MessageGroupServices : BaseServices<MessageGroup>, IMessageGroupServices
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="unitOfWork"></param>
        public MessageGroupServices(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        /// <summary>
        /// Save message group
        /// </summary>
        /// <param name="message"></param>
        protected override void Save(MessageGroup message)
        {
            if (message.Id > 0)
                this.Update(message);
            else
                this.Insert(message);
            this.SaveChanges();
        }

        /// <summary>
        /// Add a new message group in db
        /// </summary>
        /// <param name="newMessageGroup">The message group to be added</param>
        public void AddMessageGroup(MessageGroup newMessageGroup)
        {
            this.Save(newMessageGroup);
        }

        /// <summary>
        /// Update a message group
        /// </summary>
        /// <param name="message"></param>
        public void UpdateMessageGroup(MessageGroup message)
        {
            this.Save(message);
        }

        /// <summary>
        /// Delete an message group
        /// </summary>
        /// <param name="id">Id of the message group to be deleted</param>
        public new void Delete(int id)
        {
            var messageToDelete = this.GetById(id);
            base.Delete(messageToDelete);
            this.SaveChanges();
        }

    }
}