//////////////////////////////////////////////////////////////////////////////////////////////////////
//                                                                                                  //
// This class must only contains DB related info!                                                   //
// If you need to add a method or a field, go into Models and find/create the partial class         //
//                                                                                                  //
//////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using Streameus.Enums;

namespace Streameus.Models
{
    /// <summary>
    /// Represent an event item (or placeholder)
    /// </summary>
    public partial class EventItem
    {
        /// <summary>
        /// Event item ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Target Id
        /// </summary>
        public int TargetId { get; set; }

        /// <summary>
        /// Target Type
        /// </summary>
        public Enums.DataBaseEnums.EventItemType TargetType { get; set; }

        /// <summary>
        /// Position of the event Item (order by pos)
        /// </summary>
        public int Pos { get; set; }

        /// <summary>
        /// Value to show
        /// </summary>
        public string Content { get; set; }
    }
}