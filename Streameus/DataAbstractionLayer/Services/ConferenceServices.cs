using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        private readonly IRoomServices _roomServices;
        private readonly IPaymentServices _paymentServices;

        /// <summary>
        /// default constructor
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="conferenceParametersServices"></param>
        /// <param name="eventServices"></param>
        /// <param name="userServices"></param>
        /// <param name="roomServices"></param>
        /// <param name="paymentServices"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public ConferenceServices(IUnitOfWork unitOfWork, IConferenceParametersServices conferenceParametersServices,
            IEventServices eventServices, IUserServices userServices, IRoomServices roomServices,
            IPaymentServices paymentServices)
            : base(unitOfWork)
        {
            if (unitOfWork == null) throw new ArgumentNullException("unitOfWork");
            if (conferenceParametersServices == null) throw new ArgumentNullException("conferenceParametersServices");
            if (eventServices == null) throw new ArgumentNullException("eventServices");
            if (userServices == null) throw new ArgumentNullException("userServices");
            if (roomServices == null) throw new ArgumentNullException("roomServices");
            if (paymentServices == null) throw new ArgumentNullException("paymentServices");
            this._conferenceParametersServices = conferenceParametersServices;
            this._eventServices = eventServices;
            this._userServices = userServices;
            this._roomServices = roomServices;
            this._paymentServices = paymentServices;
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
        /// Get popular confs comming soon
        /// </summary>
        /// <returns></returns>
        public IQueryable<Conference> GetSoonConfs()
        {
            return
                this.GetDbSet<Conference>()
                    .Where(c => c.Time > DateTime.Now)
                    .OrderByDescending(c => c.Participants.Count)
                    .ThenBy(c => c.Time);
        }

        /// <summary>
        /// Get all users participating to a conference
        /// </summary>
        /// <param name="id">The conference Id</param>
        /// <returns></returns>
        public IQueryable<User> GetParticipantsByConferenceId(int id)
        {
            return this.GetById(id).Participants.AsQueryable();
        }

        /// <summary>
        /// Get all users registered to a conference
        /// </summary>
        /// <param name="id">The conference Id</param>
        /// <returns></returns>
        public IQueryable<User> GetRegisteredUsersByConferenceId(int id)
        {
            return this.GetById(id).Registred.AsQueryable();
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

            if (conference.OwnerId == userId)
                throw new ForbiddenException(Translation.OwnerCannotSuscribeToItsConf);

            if (conference.Time > DateTime.Now || conference.Status == DataBaseEnums.ConfStatus.EnCours ||
                conference.Status == DataBaseEnums.ConfStatus.AVenir)
            {
                if (!conference.Registred.Contains(user))
                    conference.Registred.Add(user);
                else
                    throw new DuplicateEntryException(Translation.UserHasAlreadySuscribed);
                this.Save(conference);
                this._eventServices.SuscribeConf(user, conference);
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

        /// <summary>
        /// Get all the conference the user suscribed to which are live
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IEnumerable<Conference> GetLiveConferenceForUser(int userId)
        {
            var user = this._userServices.GetById(userId);
            var involved = user.ConferencesInvolved.Where(
                c => c.Time <= DateTime.Now && c.Status == DataBaseEnums.ConfStatus.EnCours);
            var created = user.ConferencesCreated.Where(
                c => c.Time <= DateTime.Now && c.Status == DataBaseEnums.ConfStatus.EnCours);
            var registred = user.ConferencesRegistered.Where(
                c => c.Time <= DateTime.Now && c.Status == DataBaseEnums.ConfStatus.EnCours);
            return created.Union(registred).Union(involved);
        }

        /// <summary>
        /// Get all the conferences the user suscribed to happening in the next 24hours
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IEnumerable<Conference> GetSoonConferenceForUser(int userId)
        {
            var user = this._userServices.GetById(userId);
            var today = DateTime.Now;
            var tomorrow = DateTime.Now.AddDays(1);
            return
                user.ConferencesRegistered.Where(
                    c => c.Time >= today && c.Time <= tomorrow);
        }

        /// <summary>
        /// Start a conference, change its status from AVenir to EnCours
        /// </summary>
        /// <remarks>User needs to be the owner</remarks>
        /// <param name="confId">the Id of the conference</param>
        /// <param name="userId">the Id of the user who wants tho make the change</param>
        /// <returns>True if success false otherwise</returns>
        public Conference StartConference(int confId, int userId)
        {
            var conference = this.GetById(confId);
            if (conference.OwnerId != userId)
                return null;
            if (conference.Status == DataBaseEnums.ConfStatus.AVenir)
            {
                conference.RoomId = this._roomServices.CreateRoom(conference.Name).Result;
                conference.Status = DataBaseEnums.ConfStatus.EnCours;
                this.UpdateConference(conference, userId);
                return conference;
            }
            return null;
            //todo Gerer les autre cas autrements
        }

        /// <summary>
        /// Stop a conference, change its status from EnCours to Finie
        ///Gives the money to the conference owner
        /// </summary>
        /// <remarks>User needs to be the owner</remarks>
        /// <param name="confId">the Id of the conference</param>
        /// <param name="userId">the Id of the user who wants tho make the change</param>
        /// <returns>True if success false otherwise</returns>        
        public bool StopConference(int confId, int userId)
        {
            var conference = this.GetById(confId);
            if (conference.OwnerId != userId)
                return false;
            if (conference.Status == DataBaseEnums.ConfStatus.EnCours)
            {
                conference.Status = DataBaseEnums.ConfStatus.Finie;
                conference.RoomId = null;
                this.UpdateConference(conference, userId);
                this.PayConferenceOwner(conference);
                return true;
            }
            return false;
            //todo Gerer les autre cas autrements
        }

        //We take a 10% fee on every conf
        private void PayConferenceOwner(Conference conference)
        {
            var participantsNumber = conference.Participants.Count;
            float total = (conference.EntranceFee*participantsNumber)*0.9f;
            conference.Owner.Balance += total;
            this._userServices.UpdateUser(conference.Owner);
        }

        /// <summary>
        /// Check if the user is registered to the conf
        /// </summary>
        /// <param name="userId">The user ID</param>
        /// <param name="confId">The conf ID</param>
        /// <returns></returns>
        public bool IsUserRegistered(int confId, int userId)
        {
            var conf = this.GetById(confId);
            return conf.Registred.Any(u => u.Id == userId);
        }

        /// <summary>
        /// Get the token needed to access an active conference
        /// </summary>
        /// <param name="id">the conference ID</param>
        /// <param name="userId">the id of the user who needs a token</param>
        /// <returns></returns>
        public string GetTokenForConference(int id, int userId)
        {
            var conference = this.GetById(id);
            var user = this._userServices.GetById(userId);
            var isOwner = conference.OwnerId == userId;
            var role = isOwner ? "presenter" : "viewerWithData";

            if (conference.Status == DataBaseEnums.ConfStatus.EnCours)
            {
                if (!isOwner && !conference.Participants.Contains(user))
                {
                    conference.Participants.Add(user);
                    this.Save(conference);
                    this._paymentServices.ChargeUser(user, conference.EntranceFee);
                }
                return this._roomServices.CreateToken(conference.RoomId, user.UserName, role);
            }
            throw new ApiException(HttpStatusCode.Unauthorized, Translation.TheConferenceIsNotStarted);
        }
    }
}