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
    }
}