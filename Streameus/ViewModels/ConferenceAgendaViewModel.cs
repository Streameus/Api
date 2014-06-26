using System;
using Streameus.Models;

namespace Streameus.ViewModels
{
    /// <summary>
    /// Class returned as a list of conferences to the client
    /// </summary>
    public class ConferenceAgendaViewModel
    {
        /// <summary>
        /// Id conf
        /// </summary>
        public int Id;

        /// <summary>
        /// Name conf
        /// </summary>
        public string Name;

        /// <summary>
        /// Date conf
        /// </summary>
        public DateTime Date;

        /// <summary>
        /// Default constructor
        /// </summary>
        public ConferenceAgendaViewModel()
        {
        }

        /// <summary>
        /// Construct the object from a conference
        /// </summary>
        /// <param name="c"></param>
        public ConferenceAgendaViewModel(Conference c)
        {
            Id = c.Id;
            Name = c.Name;
            Date = c.Time;
        }
    }
}