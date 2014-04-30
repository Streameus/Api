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
        private readonly IMessageGroupServices _messageGroupServices;
        private readonly IUserServices _userServices;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="messageServices"></param>
        /// <param name="userServices"></param>
        public MessageController(IMessageServices messageServices, IMessageGroupServices messageGroupServices, IUserServices userServices)
        {
            if (messageServices == null) throw new ArgumentNullException("messageServices");
            this._messageServices = messageServices;
            if (messageGroupServices == null) throw new ArgumentNullException("messageGroupServices");
            this._messageGroupServices = messageGroupServices;
            if (userServices == null) throw new ArgumentNullException("userServices");
            this._userServices = userServices;
        }

        // POST api/message
        /// <summary>
        /// Create a new message
        /// </summary>
        /// <param name="newMessageViewModel"></param>
        /// <returns></returns>
        /// <exception cref="ConflictdException">An message already exist with same infos</exception>
        public MessageViewModel Post([FromBody] NewMessageViewModel newMessageViewModel)
        {
            MessageGroup msgGroup;
            // Add to a message group
            if (newMessageViewModel.MessageGroupId > 0)
            {
                msgGroup = _messageGroupServices.GetById(newMessageViewModel.MessageGroupId);
            }
            // Create a new message group
            else
            {
                msgGroup = new MessageGroup();
                foreach (var recipientId in newMessageViewModel.Recipients)
                {
                    var user = this._userServices.GetById(recipientId);
                    msgGroup.Members.Add(user);
                }
                this._messageGroupServices.AddMessageGroup(msgGroup);
            }
            var sender = _userServices.GetById(Convert.ToInt32(this.User.Identity.GetUserId()));
            var msg = new Message
            {
                Content = newMessageViewModel.Content,
                Date = DateTime.Now,
                Sender = sender,
                Group = msgGroup,
            };
            _messageServices.AddMessage(msg);
            return new MessageViewModel(msg);
        }

    }
}