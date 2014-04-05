using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Streameus.DataAbstractionLayer.Contracts;
using Streameus.Exceptions;
using Streameus.Exceptions.HttpErrors;
using Streameus.Models;
using Streameus.ViewModels;
using WebGrease.Css.Extensions;
using NoResultException = Streameus.Exceptions.NoResultException;

namespace Streameus.Controllers
{
    /// <summary>
    /// MessageGroup controller
    /// </summary>
    [RoutePrefix("api/MessageGroup")]
    public class MessageGroupController : BaseController
    {
        private readonly IMessageGroupServices _messageGroupServices;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="messageGroupServices"></param>
        public MessageGroupController(IMessageGroupServices messageGroupServices)
        {
            if (messageGroupServices == null) throw new ArgumentNullException("messageGroupServices");
            this._messageGroupServices = messageGroupServices;
        }

        // GET api/messageGroup
        /// <summary>
        /// Get all the messageGroups
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NoResultException"></exception>
        public IEnumerable<MessageGroupViewModel> Get()
        {
            var messageGroupList = new List<MessageGroupViewModel>();
            this._messageGroupServices.GetAll().ForEach(u => messageGroupList.Add(new MessageGroupViewModel(u)));
            if (!messageGroupList.Any())
                throw new NoResultException("Empty Set");
            return messageGroupList;
        }

        // GET api/messageGroup/5
        /// <summary>
        /// Get one messageGroup
        /// </summary>
        /// <param name="id">the id of the messageGroup to get</param>
        /// <returns></returns>
        public MessageGroupViewModel Get(int id)
        {
            var messageGroup = this._messageGroupServices.GetById(id);
            return new MessageGroupViewModel(messageGroup);
        }

        // POST api/messageGroup
        /// <summary>
        /// Create a new messageGroup
        /// </summary>
        /// <param name="messageGroupViewModel"></param>
        /// <returns></returns>
        public MessageGroupViewModel Post([FromBody] MessageGroupViewModel messageGroupViewModel)
        {
            messageGroupViewModel.Id = 0;
            var newMessageGroup = Mapper.Map<MessageGroup>(messageGroupViewModel);
            this._messageGroupServices.AddMessageGroup(newMessageGroup);
            return new MessageGroupViewModel(newMessageGroup);
        }

        // DELETE api/messageGroup/5
        /// <summary>
        /// Delete a messageGroup
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            this._messageGroupServices.Delete(id);
        }

        /// <summary>
        /// Get all messages of a group
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("{id}/messages")]
        public IEnumerable<MessageViewModel> GetMessagesGroupOfUser(int id)
        {
            var messages = this._messageGroupServices.GetById(id).Messages;
            var messagesList = new List<MessageViewModel>();
            messages.ForEach(m => messagesList.Add(new MessageViewModel(m)));
            return messagesList;
        }

    }
}