﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Streameus.Models;

namespace Streameus.ViewModels
{
    /// <summary>
    /// The event view model returned to the client
    /// </summary>
    public class EventViewModel
    {
        /// <summary>
        /// Event Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Type of event
        /// </summary>
        public Enums.DataBaseEnums.EventType Type { get; set; }

        /// <summary>
        /// Date this event happened
        /// </summary>
        public System.DateTime Date { get; set; }

        /// <summary>
        /// Id of the user who generated this event
        /// </summary>
        public int AuthorId { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public EventViewModel()
        {
        }

        /// <summary>
        /// Instantiate VM with a user's values
        /// </summary>
        /// <param name="obj"></param>
        public EventViewModel(Event obj)
        {
            this.Id = obj.Id;
            this.Type = obj.Type;
            this.Date = obj.Date;
            this.AuthorId = obj.AuthorId;
        }
    }
}