using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Resources;

namespace Streameus.Enums
{
    /// <summary>
    /// This class contains all the enums used for db storage
    /// </summary>
    public class DataBaseEnums
    {
        /// <summary>
        /// Used to describe the status of the conference
        /// </summary>
        public enum ConfStatus
        {
            /// <summary>
            /// Conf is ongoing
            /// </summary>
            EnCours = 1,

            /// <summary>
            /// Conf is to be started
            /// </summary>
            AVenir = 2,

            /// <summary>
            /// Conf is done
            /// </summary>
            Finie = 3,
        }

        /// <summary>
        /// Used to describe the type of an event
        /// </summary>
        public enum EventType
        {
            /// <summary>
            /// BilanCreateConfMonth
            /// </summary>
            /// 
            [Display(ResourceType = typeof (Resources.Events), Name = "BilanCreateConfMonth")] BilanCreateConfMonth = 1,

            /// <summary>
            /// BilanCreateConfWeek
            /// </summary>
            [Display(Name = "BilanCreateConfWeek", ResourceType = typeof (Events))] BilanCreateConfWeek,

            /// <summary>
            /// BilanFollowers
            /// </summary>
            [Display(Name = "BilanFollowers", ResourceType = typeof (Events))] BilanFollowers,

            /// <summary>
            /// BilanFollowingMonth
            /// </summary>
            [Display(Name = "BilanFollowingMonth", ResourceType = typeof (Events))] BilanFollowingMonth,

            /// <summary>
            /// BilanFollowingWeek
            /// </summary>
            [Display(Name = "BilanFollowingWeek", ResourceType = typeof (Events))] BilanFollowingWeek,

            /// <summary>
            /// BilanParticipateConfMonth
            /// </summary>
            [Display(Name = "BilanParticipateConfMonth", ResourceType = typeof (Events))] BilanParticipateConfMonth,

            /// <summary>
            /// BilanParticipateConfWeek
            /// </summary>
            [Display(Name = "BilanParticipateConfWeek", ResourceType = typeof (Events))] BilanParticipateConfWeek,

            /// <summary>
            /// BilanReputation
            /// </summary>
            [Display(Name = "BilanReputation", ResourceType = typeof (Events))] BilanReputation,

            /// <summary>
            /// CreateConf
            /// </summary>
            [Display(Name = "CreateConf", ResourceType = typeof (Events))] CreateConf,

            /// <summary>
            /// FirstFollower
            /// </summary>
            [Display(Name = "FirstFollower", ResourceType = typeof (Events))] FirstFollower,

            /// <summary>
            /// ParticipateConf
            /// </summary>
            [Display(Name = "ParticipateConf", ResourceType = typeof (Events))] ParticipateConf,

            /// <summary>
            /// RateConf
            /// </summary>
            [Display(Name = "RateConf", ResourceType = typeof (Events))] RateConf,

            /// <summary>
            /// StartFollow
            /// </summary>
            [Display(Name = "StartFollow", ResourceType = typeof (Events))] StartFollow,

            /// <summary>
            /// SuscribeConf
            /// </summary>
            [Display(Name = "SuscribeConf", ResourceType = typeof (Events))] SuscribeConf,

            /// <summary>
            /// UpdateMsgPerso
            /// </summary>
            [Display(Name = "UpdateMsgPerso", ResourceType = typeof (Events))] UpdateMsgPerso,

            /// <summary>
            /// ConferenceStart
            /// </summary>
            [Display(Name = "ConferenceStart", ResourceType = typeof(Events))] ConferenceStart,
            
            /// <summary>
            /// WillParticipateConf
            /// </summary>
            [Display(Name = "WillParticipateConf", ResourceType = typeof(Events))] WillParticipateConf,

            /// <summary>
            /// ConferenceEnd
            /// </summary>
            [Display(Name = "ConferenceEnd", ResourceType = typeof(Events))] ConferenceEnd,

            /// <summary>
            /// UserInvitation
            /// </summary>
            [Display(Name = "UserInvitation", ResourceType = typeof(Events))] UserInvitation,

            /// <summary>
            /// Recommand
            /// </summary>
            [Display(Name = "Recommand", ResourceType = typeof(Events))] Recommand,

            /// <summary>
            /// ShareConf
            /// </summary>
            [Display(Name = "ShareConf", ResourceType = typeof(Events))] ShareConf,

            /// <summary>
            /// ConfTitle
            /// </summary>
            [Display(Name = "ConfTitle", ResourceType = typeof(Events))] ConfTitle,

            /// <summary>
            /// ConfDate
            /// </summary>
            [Display(Name = "ConfDate", ResourceType = typeof(Events))] ConfDate,

            /// <summary>
            /// ConfTime
            /// </summary>
            [Display(Name = "ConfTime", ResourceType = typeof(Events))] ConfTime,

            /// <summary>
            /// ChangePhoto
            /// </summary>
            [Display(Name = "ChangePhoto", ResourceType = typeof(Events))] ChangePhoto,

            /// <summary>
            /// ConfDescription
            /// </summary>
            [Display(Name = "ConfDescription", ResourceType = typeof(Events))] ConfDescription,
        }

        /// <summary>
        /// Used to describe to type of an event item
        /// </summary>
        public enum EventItemType
        {
            /// <summary>
            /// User
            /// </summary>
            User = 1,

            /// <summary>
            /// Comment
            /// </summary>
            Comment,

            /// <summary>
            /// Conference
            /// </summary>
            Conference,

            /// <summary>
            /// Integer
            /// </summary>
            Int,

            /// <summary>
            /// DateTime
            /// </summary>
            DateTime,

            /// <summary>
            /// String
            /// </summary>
            String,
        }
    }
}