using System.Collections.Generic;

namespace Streameus.ViewModels
{
    /// <summary>
    /// The ViewModel used to return all the notifications
    /// </summary>
    public class NotificationsViewModel
    {
        /// <summary>
        /// The user unread messages count
        /// </summary>
        public int UnreadMessages { get; set; }

        /// <summary>
        /// All the conferences the user suscribed to which are live
        /// </summary>
        public IEnumerable<ConferenceAgendaViewModel> Live { get; set; }

        /// <summary>
        /// All the conferences the user suscribed to happening in the next 24h
        /// </summary>
        public IEnumerable<ConferenceAgendaViewModel> Soon { get; set; }
    }
}