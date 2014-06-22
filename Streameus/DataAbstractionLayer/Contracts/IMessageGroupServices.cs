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
    /// The interface for the message group services
    /// </summary>
    public interface IMessageGroupServices : IBaseServices<MessageGroup>
    {
        /// <summary>
        /// Add a new message group
        /// </summary>
        /// <param name="newMessageGroup">the message group to be added</param>
        /// <exception cref="DuplicateEntryException">A message group already exists with the same users</exception>
        void AddMessageGroup(MessageGroup newMessageGroup);

        /// <summary>
        /// Delete a message group
        /// </summary>
        /// <param name="id">Id of the message group to be deleted</param>
        void Delete(int id);
    }
}