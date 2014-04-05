using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Streameus.Exceptions;
using Streameus.Models;

namespace Streameus.DataAbstractionLayer.Contracts
{
    /// <summary>
    /// The interface for the message services
    /// </summary>
    public interface IMessageServices : IBaseServices<Message>
    {
        /// <summary>
        /// Add a new message
        /// </summary>
        /// <param name="newMessage">the message to be added</param>
        /// <exception cref="DuplicateEntryException">A message already exists with the same pseudo or email</exception>
        void AddMessage(Message newMessage);

        /// <summary>
        /// Update a new message
        /// </summary>
        /// <param name="message">The message to be updated</param>
        /// <exception cref="DuplicateEntryException"></exception>
        void UpdateMessage(Message message);

        /// <summary>
        /// Delete an message
        /// </summary>
        /// <param name="id">Id of the message to be deleted</param>
        void Delete(int id);
    }
}