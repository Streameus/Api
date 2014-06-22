using System;

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
    }
}