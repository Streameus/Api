using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.OData.Query;
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
        public MessageController(IMessageServices messageServices, IMessageGroupServices messageGroupServices,
            IUserServices userServices)
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
        /// <param name="options">options for sorting and filtering</param>
        /// <returns></returns>
        /// <exception cref="NoResultException"></exception>
        [Route("Group")]
        public IEnumerable<MessageGroupViewModel> GetGroups(ODataQueryOptions<MessageGroup> options)
        {
            var messageGroupList = new List<MessageGroupViewModel>();
            var groups = options.ApplyTo(this._messageGroupServices.GetAll().AsQueryable()) as IQueryable<MessageGroup>;
            groups.ForEach(u => messageGroupList.Add(new MessageGroupViewModel(u)));
            return messageGroupList;
        }

        /// <summary>
        /// Return the currently authenticated user message groups
        /// </summary>
        /// <param name="options">options for sorting and filtering</param>
        /// <returns></returns>
        [Route("Group/My")]
        [Authorize]
        public IEnumerable<MessageGroupViewModel> GetMy(ODataQueryOptions<MessageGroupViewModel> options)
        {
            var userId = this.GetCurrentUserId();
            var userGroups = this._messageGroupServices.GetAll().Where(g => g.Members.Any(m => m.Id == userId)).AsQueryable();
            var messageGroupList = new List<MessageGroupViewModel>();
            userGroups.ForEach(u => messageGroupList.Add(new MessageGroupViewModel(u, userId)));
            return options.ApplyTo(messageGroupList.AsQueryable()) as IQueryable<MessageGroupViewModel>;
        }

        // GET api/message/group/5
        /// <summary>
        /// Get one messageGroup
        /// </summary>
        /// <param name="id">the id of the messageGroup to get</param>
        /// <param name="options">options for sorting and filtering</param>
        /// <returns></returns>
        [Authorize]
        [Route("Group/{id}")]
        public MessageGroupViewModel GetGroup(int id, ODataQueryOptions<Message> options)
        {
            var messageGroup = this._messageGroupServices.GetById(id);
            var user = this.CheckUser(messageGroup);
            var totalMsgs = messageGroup.Messages.Count;
            var sortedMessages = options.ApplyTo(messageGroup.Messages.AsQueryable()) as IQueryable<Message>;
            if (sortedMessages != null)
                messageGroup.Messages = sortedMessages.ToArray();
            return new MessageGroupViewModel(messageGroup, user.Id, totalMsgs);
        }

        /// <summary>
        /// Get all messages of a group
        /// </summary>
        /// <param name="id">id of the message group</param>
        /// <param name="options">options for sorting and filtering</param>
        /// <returns></returns>
        [Route("{id}")]
        public IEnumerable<MessageViewModel> GetMessages(int id, ODataQueryOptions<Message> options)
        {
            var group = this._messageGroupServices.GetById(id);
            var user = this.CheckUser(group);
            if (group.UnreadBy.Contains(user))
            {
                group.UnreadBy.Remove(user);
                this._messageGroupServices.UpdateMessageGroup(group);
            }
            var messages = group.Messages.AsQueryable();
            messages = options.ApplyTo(messages) as IQueryable<Message>;
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
        [Authorize]
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
                    msgGroup.Members.Add(this._userServices.GetById(recipientId));
                }
                this._messageGroupServices.AddMessageGroup(msgGroup);
            }
            var user = this.CheckUser(msgGroup);
            msgGroup.UnreadBy.Clear();
            this._messageGroupServices.UpdateMessageGroup(msgGroup);
            msgGroup.Members.Where(i => i != user).ForEach(i => msgGroup.UnreadBy.Add(i));
            this._messageGroupServices.UpdateMessageGroup(msgGroup);
            var sender = _userServices.GetById(Convert.ToInt32(this.User.Identity.GetUserId()));
            var msg = new Message
            {
                Content = System.Uri.UnescapeDataString(newMessageViewModel.Content.Trim()),
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
        /// <param name="options">options for sorting and filtering</param>
        /// <returns></returns>
        [Route("Group")]
        public NewMessageGroupViewModel Post(int[] userIds, ODataQueryOptions<Message> options)
        {
            var userId = this.GetCurrentUserId();
            var users = userIds.Select(id => this._userServices.GetById(id)).ToList();
            if (users.Count == 1 && users.First().Id == userId)
                throw new InvalidOperationException("You cannot send a message to yourself");
            var messageGroups = this._messageGroupServices.GetAll();
            try
            {
                var existingGroup =
                    messageGroups.SingleOrDefault(g => g.Members.Intersect(users).Count() == users.Count());
                // Search if a group already exists with those users
                if (existingGroup != null)
                {
                    // Sorting messages
                    var totalMsgs = existingGroup.Messages.Count;
                    var sortedMessages = options.ApplyTo(existingGroup.Messages.AsQueryable()) as IQueryable<Message>;
                    if (sortedMessages != null)
                        existingGroup.Messages = sortedMessages.ToArray();
                    return new NewMessageGroupViewModel(existingGroup, userId, totalMsgs);
                }
                // Create a new one
                var group = new MessageGroup {Members = users};
                this._messageGroupServices.AddMessageGroup(group);
                return new NewMessageGroupViewModel(group, userId);
            }
            catch (Exception e)
            {
                throw new Exceptions.HttpErrors.NoResultException(e.Message);
            }
        }

        /// <summary>
        /// Check if the authenticated user can access to this message group
        /// </summary>
        /// <param name="group">Message group</param>
        private User CheckUser(MessageGroup group)
        {
            var userId = this.GetCurrentUserId();
            var user = this._userServices.GetById(userId);
            if (!group.Members.Contains(user))
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
            return user;
        }
    }
}