using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;
using Streameus.Exceptions;
using Streameus.Models;

namespace Streameus.DataAbstractionLayer.Contracts
{
    /// <summary>
    /// The interface for the event services
    /// </summary>
    public interface IEventServices : IBaseServices<Event>
    {
        /// <summary>
        /// Add a new event in db
        /// </summary>
        /// <param name="evt">The event to be added</param>
        void AddEvent(Event evt);

        /// <summary>
        /// get events with event items
        /// </summary>
        /// <returns></returns>
        IQueryable<Event> GetAllWithIncludes();

        /// <summary>
        /// Return all the events for the specified user
        /// </summary>
        /// <param name="userId">userId</param>
        /// <returns>A list of all the events</returns>
        /// <exception cref="EmptyResultException">If the user has no events</exception>
        /// <exception cref="NoResultException">If author doesnt exists</exception>
        IQueryable<Event> GetEventsForUser(int userId);

        /// <summary>
        /// Create start following event
        /// </summary>
        /// <param name="user1">The user who wants a following</param>
        /// <param name="user2">The user who is followed</param>
        void StartFollowing(User user1, User user2);

        /// <summary>
        /// Create create conference event
        /// </summary>
        /// <param name="conf">The conference</param>
        void CreateConf(Conference conf);

        //////


        /// <summary>
        /// Create bilan create conf month event
        /// </summary>
        /// <param name="user">The user who created the conferences</param>
        /// <param name="nbConf">The number of conference created by the user this month</param>
        void BilanCreateConfMonth(User user, int nbConf);

        /// <summary>
        /// Create BilanCreateConfWeek event
        /// </summary>
        /// <param name="user">The user who created the conferences</param>
        /// <param name="nbConf">The number of conference created by the user this week</param>
        void BilanCreateConfWeek(User user, int nbConf);

        /// <summary>
        /// Create BilanFollowers event
        /// </summary>
        /// <param name="user">The user who has followers</param>
        void BilanFollowers(User user);

        /// <summary>
        /// Create BilanFollowingMonth event
        /// </summary>
        /// <param name="user">The user who follows people</param>
        /// <param name="nbFollowing">number of people who have been following during the month</param>
        void BilanFollowingMonth(User user, int nbFollowing);

        /// <summary>
        /// Create BilanFollowingWeek event
        /// </summary>
        /// <param name="user">The user who follows people</param>
        /// <param name="nbFollowing">number of people who have been following during the week</param>
        void BilanFollowingWeek(User user, int nbFollowing);

        /// <summary>
        /// Create BilanParticipateConfMonth event
        /// </summary>
        /// <param name="user">The user who participates to the conferences</param>
        /// <param name="nbConf">The number of conferences the user has participated this month</param>
        void BilanParticipateConfMonth(User user, int nbConf);

        /// <summary>
        /// Create BilanParticipateConfWeek event
        /// </summary>
        /// <param name="user">The user who participates to the conferences</param>
        /// <param name="nbConf">The number of conferences the user has participated this week</param>
        void BilanParticipateConfWeek(User user, int nbConf);

        /// <summary>
        /// Create BilanReputation event
        /// </summary>
        /// <param name="user">A user to display reputation</param>
        void BilanReputation(User user);

        /// <summary>
        /// Create ChangePhoto event
        /// </summary>
        /// <param name="user">The user who changed his photo</param>
        void ChangePhoto(User user);

        /// <summary>
        /// Create ConfDate event
        /// </summary>
        /// <param name="conf">The conference which has been modified (date)</param>
        void ConfDate(Conference conf);

        /// <summary>
        /// Create ConfDescription event
        /// </summary>
        /// <param name="conf">The conference which has been modified (description)</param>
        void ConfDescription(Conference conf);

        /// <summary>
        /// Create ConferenceEnd event
        /// </summary>
        /// <param name="conf">The conference is ended</param>
        void ConferenceEnd(Conference conf);

        /// <summary>
        /// Create ConferenceStart event
        /// </summary>
        /// <param name="conf">The conference is beginning</param>
        void ConferenceStart(Conference conf);

        /// <summary>
        /// Create ConfTime event
        /// </summary>
        /// <param name="user">The user who modified the conference</param>
        /// <param name="conf">The conference which has been modified (time)</param>
        void ConfTime(User user, Conference conf);

        /// <summary>
        /// Create ConfTitle event
        /// </summary>
        /// <param name="user">The user who modified the conference</param>
        /// <param name="conf">The conference which has been modified (title)</param>
        void ConfTitle(User user, Conference conf);

        /// <summary>
        /// Create FirstFollower event
        /// </summary>
        /// <param name="user1">The user who is followed</param>
        /// <param name="user2">The user who is following</param>
        void FirstFollower(User user1, User user2);

        /// <summary>
        /// Create ParticipateConf event
        /// </summary>
        /// <param name="user">The user who participated to the conference</param>
        /// <param name="conf">The conference</param>
        void ParticipateConf(User user, Conference conf);

        /// <summary>
        /// Create RateConf event
        /// </summary>
        /// <param name="user">The user who is rating the conference</param>
        /// <param name="stars">Number of stars</param>
        /// <param name="conf">The conference which is rated</param>
        void RateConf(User user, int stars, Conference conf);

        /// <summary>
        /// Create Recommand event
        /// </summary>
        /// <param name="user">The user who recommands the conference</param>
        /// <param name="conf">The conference which is recommanded</param>
        void Recommand(User user, Conference conf);

        /// <summary>
        /// Create ShareConf event
        /// </summary>
        /// <param name="user">The user who shared the conference</param>
        /// <param name="conf">The conference which is shared</param>
        void ShareConf(User user, Conference conf);

        /// <summary>
        /// Create SuscribeConf event
        /// </summary>
        /// <param name="user">The user who participates at the conference</param>
        /// <param name="conf">The conference</param>
        void SuscribeConf(User user, Conference conf);

        /// <summary>
        /// Create UpdateMsgPerso event
        /// </summary>
        /// <param name="user">The user who modified his personnal message</param>
        void UpdateMsgPerso(User user);

        /// <summary>
        /// Create UserInvitation event
        /// </summary>
        /// <param name="user">The user who made the invitation</param>
        /// <param name="conf">The conference</param>
        void UserInvitation(User user, Conference conf);

        /// <summary>
        /// Create WillParticipateConf event
        /// </summary>
        /// <param name="user">The user who will participate to the conference</param>
        /// <param name="conf">The conference</param>
        void WillParticipateConf(User user, Conference conf);
    }
}