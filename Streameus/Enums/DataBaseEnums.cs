using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

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
            /// Conf is created
            /// </summary>
            ConfCree = 1,

            /// <summary>
            /// User joined conf
            /// </summary>
            ConfInscrit = 2,

            /// <summary>
            /// Follow event
            /// </summary>
            Follow = 3,
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