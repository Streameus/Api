﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Streameus.Exceptions;
using Streameus.Models;

namespace Streameus.DataAbstractionLayer.Contracts
{
    /// <summary>
    /// Interface for the conference services
    /// </summary>
    public interface IConferenceServices : IBaseServices<Conference>
    {
        /// <summary>
        /// Delete a conference
        /// </summary>
        /// <param name="id">The id of the conference</param>
        void Delete(int id);

        /// <summary>
        /// Add a new conference
        /// </summary>
        /// <param name="newConf">The conference to be added</param>
        void AddConference(Conference newConf);

        /// <summary>
        /// Updates a conference
        /// </summary>
        /// <param name="updatedConf"></param>
        /// <param name="userId">The id of the user who wants to update this conference</param>
        void UpdateConference(Conference updatedConf, int userId);

        /// <summary>
        /// Get conferences suggested for a user depending on its interests
        /// </summary>
        /// <param name="userId">the targeted user</param>
        /// <returns></returns>
        IEnumerable<Conference> GetSuggestionsForUser(int userId);

        /// <summary>
        /// Get the 5 most popular confs
        /// </summary>
        /// <returns></returns>
        IEnumerable<Conference> GetMostPopularConfs();

        /// <summary>
        /// Get popular confs comming soon
        /// </summary>
        /// <returns></returns>
        IQueryable<Conference> GetSoonConfs();

        /// <summary>
        /// Get all users participating to a conference
        /// </summary>
        /// <param name="id">The conference Id</param>
        /// <returns></returns>
        IQueryable<User> GetParticipantsByConferenceId(int id);

        /// <summary>
        /// Get all users registered to a conference
        /// </summary>
        /// <param name="id">The conference Id</param>
        /// <returns></returns>
        IQueryable<User> GetRegisteredUsersByConferenceId(int id);

        /// <summary>
        /// Suscribe a user to a conference
        /// </summary>
        /// <remarks>This method adds the user to the Participants List</remarks>
        /// <param name="conferenceId">The conference Id</param>
        /// <param name="userId">The user Id</param>
        /// <exception cref="DuplicateEntryException">The use has already suscribed to the conf</exception>
        void SuscribeUserToConference(int conferenceId, int userId);

        /// <summary>
        /// Unsuscribe a user from a conference
        /// </summary>
        /// <remarks>This method removes the user from the Participants List</remarks>
        /// <param name="conferenceId">The conference Id</param>
        /// <param name="userId">The user Id</param>
        /// <exception cref="DuplicateEntryException">The user is not participating to this conf</exception>
        void UnsuscribeUserFromConference(int conferenceId, int userId);

        /// <summary>
        /// Returns all the currently live conferences a user suscribed to
        /// </summary>
        /// <param name="userId">the id of the user</param>
        /// <returns></returns>
        IEnumerable<Conference> GetLiveConferenceForUser(int userId);

        /// <summary>
        /// Returns all the conferences a user suscribed to airing in the next 24 hours
        /// </summary>
        /// <param name="userId">the id of the user</param>
        /// <returns></returns>
        IEnumerable<Conference> GetSoonConferenceForUser(int userId);

        /// <summary>
        /// Start a conference, change its status from AVenir to EnCours
        /// </summary>
        /// <remarks>User needs to be the owner</remarks>
        /// <param name="confId">the Id of the conference</param>
        /// <param name="userId">the Id of the user who wants tho make the change</param>
        /// <returns>True if success false otherwise</returns>
        Conference StartConference(int confId, int userId);


        /// <summary>
        /// Stop a conference, change its status from EnCours to Finie
        /// Gives the money to the conference owner
        /// </summary>
        /// <remarks>User needs to be the owner</remarks>
        /// <param name="confId">the Id of the conference</param>
        /// <param name="userId">the Id of the user who wants tho make the change</param>
        /// <returns>True if success false otherwise</returns>        
        bool StopConference(int confId, int userId);

        /// <summary>
        /// Check if the user is registered to the conf
        /// </summary>
        /// <param name="userId">The user ID</param>
        /// <param name="confId">The conf ID</param>
        /// <returns></returns>
        bool IsUserRegistered(int confId, int userId);

        /// <summary>
        /// Get the token needed to access an active conference
        /// </summary>
        /// <param name="id">the conference ID</param>
        /// <param name="userId">the id of the user who needs a token</param>
        /// <returns></returns>
        string GetTokenForConference(int id, int userId);

        /// <summary>
        /// Mark a conference a user participated to
        /// </summary>
        /// <param name="conferenceId"></param>
        /// <param name="userId"></param>
        /// <param name="mark"></param>
        double MarkConference(int conferenceId, int userId, int mark);

        /// <summary>
        /// Get all conferences currently airing (status = EnCours)
        /// </summary>
        /// <returns>The list of conferences which a live</returns>
        IQueryable<Conference> GetLiveConferences();
    }
}