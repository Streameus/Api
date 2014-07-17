﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Streameus.App_GlobalResources;
using Streameus.DataAbstractionLayer.Contracts;
using Streameus.DataBaseAccess;
using Streameus.Enums;
using Streameus.Exceptions;
using Streameus.Exceptions.HttpErrors;
using Streameus.Models;

namespace Streameus.DataAbstractionLayer.Services
{
    /// <summary>
    /// The conference services
    /// </summary>
    public class ConferenceServices : BaseServices<Conference>, IConferenceServices
    {
        private readonly IConferenceParametersServices _conferenceParametersServices;
        private readonly IEventServices _eventServices;
        private readonly IUserServices _userServices;

        /// <summary>
        /// default constructor
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="conferenceParametersServices"></param>
        /// <param name="eventServices"></param>
        /// <param name="userServices"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public ConferenceServices(IUnitOfWork unitOfWork, IConferenceParametersServices conferenceParametersServices,
            IEventServices eventServices, IUserServices userServices)
            : base(unitOfWork)
        {
            if (unitOfWork == null) throw new ArgumentNullException("unitOfWork");
            if (conferenceParametersServices == null) throw new ArgumentNullException("conferenceParametersServices");
            if (eventServices == null) throw new ArgumentNullException("eventServices");
            if (userServices == null) throw new ArgumentNullException("userServices");
            this._conferenceParametersServices = conferenceParametersServices;
            this._eventServices = eventServices;
            this._userServices = userServices;
        }

        /// <summary>
        /// Save a conference
        /// </summary>
        /// <param name="conference"></param>
        protected override void Save(Conference conference)
        {
            if (conference.Id > 0)
                this.Update(conference);
            else
                this.Insert(conference);
            this.SaveChanges();
        }

        /// <summary>
        /// Delete a conference
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="NotFoundException"></exception>
        public new void Delete(int id)
        {
            var conference = this.GetById(id);
            if (conference == null)
                throw new NotFoundException("No such conference");
            var param = conference.ConferenceParameters;
            base.Delete(conference);
            this._conferenceParametersServices.Delete(param);
            this.SaveChanges();
        }

        /// <summary>
        /// Add a conference
        /// </summary>
        /// <param name="newConf"></param>
        public void AddConference(Conference newConf)
        {
            newConf.Id = 0;
            newConf.ConferenceParameters = new ConferenceParameters();
            this.Save(newConf);
            this._eventServices.CreateConf(newConf);
        }


        /// <summary>
        /// Updates a conference
        /// </summary>
        /// <param name="updatedConf"></param>
        /// <param name="userId">The id of the user who wants to update this conference</param>
        public void UpdateConference(Conference updatedConf, int userId)
        {
            if (userId == this.GetById(updatedConf.Id).OwnerId)
            {
                this.Save(updatedConf);
            }
            else
            {
                throw new ForbiddenException(Translation.ForbiddenConfUpdate);
            }
        }

        /// <summary>
        /// Get conferences suggested for a user depending on its interests
        /// </summary>
        /// <param name="userId">the targeted user</param>
        /// <returns></returns>
        public IEnumerable<Conference> GetSuggestionsForUser(int userId)
        {
            var user = this._userServices.GetById(userId);
            var results = user.ConferencesRegistered
                .Where(potentialConf =>
                    !user.ConferencesRegistered.Contains(potentialConf) &&
                    potentialConf.Time > DateTime.Now)
                .GroupBy(uniqueConf => uniqueConf)
                .Select(uniqueConf => new {uniqueConf.Key, Count = uniqueConf.Count()})
                .OrderByDescending(group => group.Count)
                .Take(5)
                .Select(group => group.Key);
            return results;
        }

        /// <summary>
        /// Get the 5 most popular confs
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Conference> GetMostPopularConfs()
        {
            return
                this.GetDbSet<Conference>()
                    .Where(c => c.Time > DateTime.Now)
                    .OrderByDescending(c => c.Participants.Count)
                    .Take(5);
        }

        /// <summary>
        /// Get all users participating to a conference
        /// </summary>
        /// <param name="id">The conference Id</param>
        /// <returns></returns>
        public IQueryable<User> GetParticipantsById(int id)
        {
            return this.GetById(id).Participants.AsQueryable();
        }

        /// <summary>
        /// Suscribe a user to a conference
        /// </summary>
        /// <remarks>This method adds the user to the Participants List</remarks>
        /// <param name="conferenceId">The conference Id</param>
        /// <param name="userId">The user Id</param>
        /// <exception cref="DuplicateEntryException">The use has already suscribed to the conf</exception>
        public void SuscribeUserToConference(int conferenceId, int userId)
        {
            var conference = this.GetById(conferenceId);
            var user = this._userServices.GetById(userId);

            if (conference.Time > DateTime.Now || conference.Status == DataBaseEnums.ConfStatus.EnCours ||
                conference.Status == DataBaseEnums.ConfStatus.AVenir)
            {
                if (!conference.Registred.Contains(user))
                    conference.Registred.Add(user);
                else
                    throw new DuplicateEntryException(Translation.UserHasAlreadySuscribed);

                this.Save(conference);
            }
            else
                throw new ForbiddenException(Translation.ErrorSuscribePastConference);
        }

        /// <summary>
        /// Unsuscribe a user from a conference
        /// </summary>
        /// <remarks>This method removes the user from the Participants List</remarks>
        /// <param name="conferenceId">The conference Id</param>
        /// <param name="userId">The user Id</param>
        /// <exception cref="DuplicateEntryException">The user is not participating to this conf</exception>
        public void UnsuscribeUserFromConference(int conferenceId, int userId)
        {
            var conference = this.GetById(conferenceId);
            var user = this._userServices.GetById(userId);

            if (conference.Time > DateTime.Now)
            {
                if (!conference.Registred.Contains(user))
                    throw new DuplicateEntryException(Translation.UserIsNotEnlisted);
                if (conference.Registred.Remove(user))
                {
                    this.Save(conference);
                }
                else
                {
                    throw new Exception("Can't delete user from collection");
                }
            }
            else
                throw new ForbiddenException(Translation.ErrorSuscribePastConference);
        }

        public IEnumerable<Conference> GetLiveConferenceForUser(int userId)
        {
            var user = this._userServices.GetById(userId);
            return
                user.ConferencesRegistered.Where(
                    c => c.Time > DateTime.Now && c.Status == DataBaseEnums.ConfStatus.EnCours);
        }
    }
}