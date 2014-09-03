using Streameus.Models;

namespace Streameus.ViewModels
{
    /// <summary>
    /// ViewModel used when returning only one object. Tells the user if he is registered to the conference.
    /// </summary>
    public class ConferenceViewModelUnique : ConferenceViewModel
    {
        /// <summary>
        /// Is the user registered to this conference
        /// </summary>
        public bool Registered { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="conf"></param>
        public ConferenceViewModelUnique(Conference conf) : base(conf)
        {
        }
    }
}