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
            /// bilanCreateConfMonth
            /// </summary>
            /// 
            [Display(ResourceType = typeof (Resources.Events), Name = "BilanCreateConfMonth")] BilanCreateConfMonth = 1,

            /// <summary>
            /// bilanCreateConfWeek
            /// </summary>
            [Display(Name = "BilanCreateConfWeek", ResourceType = typeof (Events))] BilanCreateConfWeek,

            /// <summary>
            /// bilanFollowers
            /// </summary>
            [Display(Name = "BilanFollowers", ResourceType = typeof (Events))] BilanFollowers,

            /// <summary>
            /// bilanFollowingMonth
            /// </summary>
            [Display(Name = "BilanFollowingMonth", ResourceType = typeof (Events))] BilanFollowingMonth,

            /// <summary>
            /// bilanFollowingWeek
            /// </summary>
            [Display(Name = "BilanFollowingWeek", ResourceType = typeof (Events))] BilanFollowingWeek,

            /// <summary>
            /// bilanParticipateConfMonth
            /// </summary>
            [Display(Name = "BilanParticipateConfMonth", ResourceType = typeof (Events))] BilanParticipateConfMonth,

            /// <summary>
            /// bilanParticipateConfWeek
            /// </summary>
            [Display(Name = "BilanParticipateConfWeek", ResourceType = typeof (Events))] BilanParticipateConfWeek,

            /// <summary>
            /// bilanReputation
            /// </summary>
            [Display(Name = "BilanReputation", ResourceType = typeof (Events))] BilanReputation,

            /// <summary>
            /// createConf
            /// </summary>
            [Display(Name = "CreateConf", ResourceType = typeof (Events))] CreateConf,

            /// <summary>
            /// FirstFollower
            /// </summary>
            [Display(Name = "FirstFollower", ResourceType = typeof (Events))] FirstFollower,

            /// <summary>
            /// participateConf
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
            /// UpdateParameter
            /// </summary>
            [Display(Name = "UpdateParameter", ResourceType = typeof (Events))] UpdateParameter,
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
            Comment = 2,

            /// <summary>
            /// Conference
            /// </summary>
            Conference = 3,
        }
    }
}