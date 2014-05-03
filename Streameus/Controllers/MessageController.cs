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
    [RoutePrefix("api/Message")]
    public class MessageController : BaseController
    {
        private readonly IMessageGroupServices _messageGroupServices;
        private readonly IMessageServices _messageServices;
        private readonly IUserServices _userServices;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="messageGroupServices"></param>
        /// <param name="messageServices"></param>
        /// <param name="userServices"></param>
        public MessageController(IMessageServices messageServices, IMessageGroupServices messageGroupServices, IUserServices userServices)
        {
            if (messageGroupServices == null) throw new ArgumentNullException("messageGroupServices");
            this._messageGroupServices = messageGroupServices;
            if (messageServices == null) throw new ArgumentNullException("messageServices");
            this._messageServices = messageServices;
            if (userServices == null) throw new ArgumentNullException("userServices");
            this._userServices = userServices;
        }

        // GET api/message/Group
        /// <summary>
        /// Get all the message groups
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NoResultException"></exception>
        [Route("Group")]
        public IEnumerable<MessageGroupViewModel> GetGroups()
        {
            var messageGroupList = new List<MessageGroupViewModel>();
            this._messageGroupServices.GetAll().ForEach(u => messageGroupList.Add(new MessageGroupViewModel(u)));
            return messageGroupList;
        }

        /// <summary>
        /// Return the currently connected user message groups
        /// </summary>
        /// <returns></returns>
        [Route("Group/My")]
        [Authorize]
        public IEnumerable<MessageGroupViewModel> GetMy()
        {
            var userId = Convert.ToInt32(this.User.Identity.GetUserId());
            var userGroups = this._messageGroupServices.GetAll().Where(g => g.Members.Any(m => m.Id == userId));
            var messageGroupList = new List<MessageGroupViewModel>();
            userGroups.ForEach(u => messageGroupList.Add(new MessageGroupViewModel(u, userId)));
            return messageGroupList.OrderByDescending(i => i.Date);
        }

        // GET api/message/group/5
        /// <summary>
        /// Get one messageGroup
        /// </summary>
        /// <param name="id">the id of the messageGroup to get</param>
        /// <returns></returns>
        [Authorize]
        [Route("Group/{id}")]
        public MessageGroupViewModel GetGroup(int id)
        {
            var messageGroup = this._messageGroupServices.GetById(id);
            return new MessageGroupViewModel(messageGroup);
        }

        /// <summary>
        /// Get all messages of a group
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("{id}")]
        public IEnumerable<MessageViewModel> GetMessages(int id)
        {
            var messages = this._messageGroupServices.GetById(id).Messages;
            var messagesList = new List<MessageViewModel>();
            messages.ForEach(m => messagesList.Add(new MessageViewModel(m)));
            return messagesList;
        }

        // POST api/Message
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

        // POST api/messageGroup
        /// <summary>
        /// Get or create a message group with users ids and return existing messages
        /// </summary>
        /// <param name="userIds">Array of users in the message group</param>
        /// <returns></returns>
        [Route("Group")]
        public NewMessageGroupViewModel Post(int[] userIds)
        {
            var userId = Convert.ToInt32(this.User.Identity.GetUserId());
            var users = userIds.Select(id => this._userServices.GetById(id)).ToList();
            var messageGroups = this._messageGroupServices.GetAll();
            try
            {
                var existingGroup = messageGroups.SingleOrDefault(g => g.Members.Intersect(users).Count() == users.Count());
                // Search if a group already exists with those users
                if (existingGroup != null)
                    return new NewMessageGroupViewModel(existingGroup, userId);
                // Create a new one
                var group = new MessageGroup { Members = users };
                this._messageGroupServices.AddMessageGroup(group);
                return new NewMessageGroupViewModel(group, userId);
            }
            catch(Exception e)
            {
                throw new Exceptions.HttpErrors.NoResultException(e.Message);
            }
        }

    }
}