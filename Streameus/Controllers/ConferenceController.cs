using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.OData.Query;
using Microsoft.AspNet.Identity;
using Streameus.App_GlobalResources;
using Streameus.DataAbstractionLayer.Contracts;
using Streameus.Enums;
using Streameus.Exceptions;
using Streameus.Exceptions.HttpErrors;
using Streameus.Models;
using Streameus.ViewModels;
using WebGrease.Css.Extensions;

namespace Streameus.Controllers
{
    /// <summary>
    /// Conference controller
    /// </summary>
    [RoutePrefix("api/Conference")]
    public class ConferenceController : BaseController
    {
        private readonly IConferenceServices _conferenceServices;
        private readonly IConferenceCategoryServices _conferenceCategoryServices;
        private readonly IUserServices _userServices;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="conferenceCategoryServices"></param>
        /// <param name="conferenceServices"></param>
        /// <param name="userServices"></param>
        public ConferenceController(IConferenceServices conferenceServices, IUserServices userServices,
            IConferenceCategoryServices conferenceCategoryServices)
        {
            if (conferenceCategoryServices == null) throw new ArgumentNullException("conferenceCategoryServices");
            if (conferenceServices == null) throw new ArgumentNullException("conferenceServices");
            if (userServices == null) throw new ArgumentNullException("userServices");
            this._conferenceCategoryServices = conferenceCategoryServices;
            this._conferenceServices = conferenceServices;
            this._userServices = userServices;
        }

        // GET api/conference
        /// <summary>
        /// Get all conferences
        /// </summary>
        /// <returns></returns>
        /// <responseCode></responseCode>
        public IEnumerable<ConferenceViewModel> Get()
        {
            var conferences = new List<ConferenceViewModel>();
            int currentUser = this.GetCurrentUserId();
            this._conferenceServices.GetAll().ForEach(c => conferences.Add(new ConferenceViewModel(c, currentUser)));
            return conferences;
        }

        // GET api/conference
        /// <summary>
        /// Get all conferences
        /// </summary>
        /// <returns></returns>
        /// <responseCode></responseCode>
        [Route("Soon")]
        public IEnumerable<ConferenceViewModel> GetSoon(ODataQueryOptions<Conference> options)
        {
            var confs = options.ApplyTo(this._conferenceServices.GetSoonConfs()) as IQueryable<Conference>;
            var conferences = new List<ConferenceViewModel>();
            var currentUser = this.GetCurrentUserId();
            confs.ForEach(c => conferences.Add(new ConferenceViewModel(c, currentUser)));
            return conferences;
        }

        // GET api/Conference/Categories
        /// <summary>
        /// Get all conference's categories
        /// </summary>
        /// <returns></returns>
        /// <responseCode></responseCode>
        [Route("Categories")]
        public IEnumerable<ConferenceCategoryViewModel> GetCategories()
        {
            var categories = new List<ConferenceCategoryViewModel>();
            this._conferenceCategoryServices.GetAll().ForEach(c => categories.Add(new ConferenceCategoryViewModel(c)));
            return categories;
        }

        // GET api/Conference/Category/5
        /// <summary>
        /// Get all conferences of one specified category
        /// </summary>
        /// <param name="id">the id of the category</param>
        /// <returns></returns>
        /// <responseCode></responseCode>
        [Route("Category/{id}")]
        public IEnumerable<ConferenceViewModel> GetByCategory(int id)
        {
            var conferences = new List<ConferenceViewModel>();
            var currentUser = this.GetCurrentUserId();
            this._conferenceServices.GetAll()
                .Where(i => i.CategoryId == id)
                .ForEach(c => conferences.Add(new ConferenceViewModel(c, currentUser)));
            return conferences;
        }

        // GET api/conference/5
        /// <summary>
        /// Get one conference
        /// </summary>
        /// <param name="id">the id of the conference</param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        public ConferenceViewModel Get(int id)
        {
            var conf = this._conferenceServices.GetById(id);
            if (conf == null)
                throw new NotFoundException(Translation.ConferenceNotFound);
            var currentUser = this.GetCurrentUserId();
            var conference = new ConferenceViewModel(conf, currentUser);

            return conference;
        }

        // POST api/conference
        /// <summary>
        /// Create a new conference
        /// </summary>
        /// <param name="conference">data for the new conference</param>
        /// <returns></returns>
        [Authorize]
        public ConferenceViewModel Post([FromBody] ConferenceFormViewModel conference)
        {
            var userId = this.GetCurrentUserId();
            var user = _userServices.GetById(userId);
            var category = _conferenceCategoryServices.GetById(conference.CategoryId);
            var newConf = new Conference()
            {
                Name = conference.Name,
                Description = conference.Description,
                ScheduledDuration = conference.ScheduledDuration,
                Time = conference.Time.Value,
                Status = DataBaseEnums.ConfStatus.AVenir,
                Owner = user,
                Category = category,
                EntranceFee = conference.EntranceFee.HasValue ? conference.EntranceFee.Value : 0
            };
            this._conferenceServices.AddConference(newConf);
            return this.Get(newConf.Id);
        }

        // PUT api/conference/5
        /// <summary>
        /// Update a conference
        /// </summary>
        /// <param name="conference">data to update the conference</param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        /// <exception cref="ForbiddenException"></exception>
        [Authorize]
        public ConferenceViewModel Put([FromBody] ConferenceFormViewModel conference)
        {
            var updatedConf = this._conferenceServices.GetById(conference.Id);
            if (updatedConf == null)
                throw new NotFoundException("Conference not found");
            updatedConf.Name = conference.Name;
            updatedConf.Description = conference.Description;
            updatedConf.ScheduledDuration = conference.ScheduledDuration;
            if (conference.EntranceFee.HasValue)
                updatedConf.EntranceFee = conference.EntranceFee.Value;
            if (conference.Time != null)
                updatedConf.Time = conference.Time.Value;
            this._conferenceServices.UpdateConference(updatedConf, this.GetCurrentUserId());
            //since EF uses references, we will have the latest version here, so it's ok to return.
            return new ConferenceViewModel(updatedConf, this.GetCurrentUserId());
        }

        // DELETE api/conference/5
        /// <summary>
        /// Delete a conference
        /// </summary>
        /// <param name="id"></param>
        [Authorize]
        public void Delete(int id)
        {
            this._conferenceServices.Delete(id);
        }

        /// <summary>
        /// Get all the user participating to a conference
        /// </summary>
        /// <param name="id">The conference Id</param>
        /// <returns></returns>
        [Authorize]
        [Route("{id}/Participants")]
        public IEnumerable<UserViewModel> GetParticipants(int id)
        {
            IQueryable<User> participantsList = this._conferenceServices.GetParticipantsByConferenceId(id);
            var participantsVmList = new List<UserViewModel>();
            participantsList.ForEach(p => participantsVmList.Add(new UserViewModel(p)));
            return participantsVmList;
        }

        /// <summary>
        /// Get all the user registered to a conference
        /// </summary>
        /// <param name="id">The conference Id</param>
        /// <param name="options">Odata options</param>
        /// <returns></returns>
        [Authorize]
        [Route("{id}/Registered")]
        public IEnumerable<UserViewModel> GetRegistered(int id, ODataQueryOptions<User> options = null)
        {
            var registeredParticipantsList = this._conferenceServices.GetRegisteredUsersByConferenceId(id);
            if (options != null)
                registeredParticipantsList = options.ApplyTo(registeredParticipantsList) as IQueryable<User>;

            var registeredParticipantsVmList = new List<UserViewModel>();
            registeredParticipantsList.ForEach(p => registeredParticipantsVmList.Add(new UserViewModel(p)));
            return registeredParticipantsVmList;
        }

        /// <summary>
        /// Suscribe to a conference
        /// </summary>
        /// <param name="id">the id of the conference you want to suscribe to</param>
        [Route("{id}/Subscribe")]
        [Authorize]
        public void GetSuscribe(int id)
        {
            try
            {
                this._conferenceServices.SuscribeUserToConference(id, this.GetCurrentUserId());
            }
            catch (DuplicateEntryException)
            {
                //We don't do anything because the result is the same
            }
        }

        /// <summary>
        /// Unsuscribe from a conference
        /// </summary>
        /// <param name="id">the id of the conference you want to unsuscribe from</param>
        [Authorize]
        [Route("{id}/Unsubscribe")]
        public void GetUnSuscribe(int id)
        {
            try
            {
                this._conferenceServices.UnsuscribeUserFromConference(id, this.GetCurrentUserId());
            }
            catch (DuplicateEntryException)
            {
                //We don't do anything because the result is the same
            }
        }

        /// <summary>
        /// Start a conf, changes its status from AVenir to enCours
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="ForbiddenException"></exception>
        [Route("{id}/Start")]
        [Authorize]
        public string PostStartConference(int id)
        {
            var conf = this._conferenceServices.StartConference(id, this.GetCurrentUserId());
            if (conf == null)
                throw new ForbiddenException(Translation.ForbiddenConfUpdate);
            return conf.RoomId;
        }

        /// <summary>
        /// Stop a conf, changes its status from EnCours to Finie
        /// Pay the conference owner
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="ForbiddenException"></exception>
        [Route("{id}/Stop")]
        [Authorize]
        public void PostStopConference(int id)
        {
            if (!this._conferenceServices.StopConference(id, this.GetCurrentUserId()))
                throw new ForbiddenException(Translation.ForbiddenConfUpdate);
        }

        /// <summary>
        /// Get the token to watch a conference
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="ForbiddenException"></exception>
        /// <exception cref="PaymentRequiredException"></exception>
        /// <response code="402">Payment required</response>
        [Route("{id}/Token")]
        [Authorize]
        public string GetTokenConference(int id)
        {
            return this._conferenceServices.GetTokenForConference(id, this.GetCurrentUserId());
        }


        // GET api/user/AmIRegistered/{id}
        /// <summary>
        /// Tells you if the user is registered to a conference
        /// </summary>
        /// <param name="id">the id of the conf to be checked</param>
        /// <param name="userId">The id of the user</param>
        /// <returns></returns>
        [Authorize]
        [Route("{id}/Registered/{userId}")]
        public Boolean GetAmIRegistered(int id, int userId)
        {
            return this._conferenceServices.IsUserRegistered(id, userId);
        }

        // GET api/user/AmIRegistered/{id}
        /// <summary>
        /// Tells you if you are registered to a conference
        /// </summary>
        /// <param name="id">the id of the conf to be checked</param>
        /// <returns></returns>
        [Authorize]
        [Route("{id}/Registered/me")]
        public Boolean GetAmIRegistered(int id)
        {
            var currentUserId = this.GetCurrentUserId();
            return this._conferenceServices.IsUserRegistered(id, currentUserId);
        }

        /// <summary>
        /// Mark a conference you have participated in
        /// </summary>
        /// <param name="id">Conference id</param>
        /// <param name="mark">The mark given to the conference</param>
        [Authorize]
        [Route("{id}/Mark")]
        public double PostMarkConference(int id, int mark)
        {
            return this._conferenceServices.MarkConference(id, this.GetCurrentUserId(), mark);
        }
    }
}