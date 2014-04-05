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
    /// Message controller
    /// </summary>
    [RoutePrefix("api/Message")]
    public class MessageController : BaseController
    {
        private readonly IMessageServices _messageServices;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="messageServices"></param>
        public MessageController(IMessageServices messageServices)
        {
            if (messageServices == null) throw new ArgumentNullException("messageServices");
            this._messageServices = messageServices;
        }

        // GET api/message
        /// <summary>
        /// Get all the messages
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NoResultException"></exception>
        public IEnumerable<MessageViewModel> Get()
        {
            var messageList = new List<MessageViewModel>();
            this._messageServices.GetAll().ForEach(u => messageList.Add(new MessageViewModel(u)));
            if (!messageList.Any())
                throw new NoResultException("Empty Set");
            return messageList;
        }

        // GET api/message/5
        /// <summary>
        /// Get one message
        /// </summary>
        /// <param name="id">the id of the message to get</param>
        /// <returns></returns>
        public MessageViewModel Get(int id)
        {
            var message = this._messageServices.GetById(id);
            return new MessageViewModel(message);
        }

        // POST api/message
        /// <summary>
        /// Create a new message
        /// </summary>
        /// <param name="messageViewModel"></param>
        /// <returns></returns>
        /// <exception cref="ConflictdException">An message already exist with same infos</exception>
        public MessageViewModel Post([FromBody] MessageViewModel messageViewModel)
        {
            messageViewModel.Id = 0;
            var newMessage = Mapper.Map<Message>(messageViewModel);
            try
            {
                this._messageServices.AddMessage(newMessage);
            }
            catch (DuplicateEntryException entryException)
            {
                throw new ConflictdException(entryException.Message);
            }
            return new MessageViewModel(newMessage);
        }

        // PUT api/message/5
        /// <summary>
        /// Update an message
        /// </summary>
        /// <param name="id"></param>
        /// <param name="messageViewModel"></param>
        /// <returns></returns>
        /// <exception cref="ConflictdException">An message already exist with same infos</exception>
        public MessageViewModel Put(int id, [FromBody] MessageViewModel messageViewModel)
        {
            var newMessage = Mapper.Map<Message>(messageViewModel);
            try
            {
                this._messageServices.UpdateMessage(newMessage);
            }
            catch (DuplicateEntryException entryException)
            {
                throw new ConflictdException(entryException.Message);
            }
            return new MessageViewModel(newMessage);
        }

        // @todo seul le sender peut supprimer son message
        // DELETE api/message/5
        /// <summary>
        /// Delete a message
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            this._messageServices.Delete(id);
        }

    }
}