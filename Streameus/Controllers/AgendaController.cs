using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Streameus.App_GlobalResources;
using Streameus.DataAbstractionLayer.Contracts;
using Streameus.Enums;
using Streameus.Exceptions;
using Streameus.Exceptions.HttpErrors;
using Streameus.Models;
using Streameus.ViewModels;
using WebGrease.Css.Extensions;


namespace Streameus.Controllers
{
    /// <summary>
    /// Agenda controller
    /// </summary>
    [RoutePrefix("api/Agenda")]
    public class AgendaController : BaseController
    {
        private readonly IConferenceServices _conferenceServices;
        private readonly IUserServices _userServices;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="conferenceServices"></param>
        /// <param name="userServices"></param>
        public AgendaController(IConferenceServices conferenceServices, IUserServices userServices)
        {
            if (conferenceServices == null) throw new ArgumentNullException("conferenceServices");
            if (userServices == null) throw new ArgumentNullException("userServices");
            this._conferenceServices = conferenceServices;
            this._userServices = userServices;
        }

        // GET api/agenda
        /// <summary>
        /// Retourne toutes les conferences A VENIR auxquelles l'utilisateur a souscrit
        /// </summary>
        /// <returns>Retourne une List Conf (int Id, DateTime Time, String Name) ou une List vide si null</returns>
        /// <responseCode></responseCode>
        [Authorize]
        public IEnumerable<ConferenceAgendaViewModel> Get()
        {
            var owner = this._userServices.GetById(this.GetCurrentUserId());
            var conferences = owner.ConferencesRegistered.OrderBy(c => c.Time);
            var confList = new List<ConferenceAgendaViewModel>();
            foreach (var conference in conferences)
            {
                if (conference.Status != DataBaseEnums.ConfStatus.Finie)
                {
                    var confInfo = new ConferenceAgendaViewModel
                    {
                        Name = conference.Name,
                        Date = conference.Time,
                        Id = conference.Id,
                    };
                    confList.Add(confInfo);
                }
            }
            return confList;
        }

        /// <summary>
        /// Get all the ongoing conferences the user suscribed to
        /// </summary>
        /// <returns></returns>
        [Route("Live")]
        [Authorize]
        public IEnumerable<ConferenceAgendaViewModel> GetLive()
        {
            var conferences =
                this._conferenceServices.GetLiveConferenceForUser(this.GetCurrentUserId())
                    .Select(c => new ConferenceAgendaViewModel() {Date = c.Time, Id = c.Id, Name = c.Name});
            return conferences;
        }

        /// <summary>
        /// Get all conference the user suscribed to airing in the next 24 hours
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [Route("Soon")]
        public IEnumerable<ConferenceAgendaViewModel> GetSoon()
        {
            var conferences =
                this._conferenceServices.GetSoonConferenceForUser(this.GetCurrentUserId())
                    .Select(c => new ConferenceAgendaViewModel() {Date = c.Time, Id = c.Id, Name = c.Name});
            return conferences;
        }

        /// <summary>
        /// Get notifications for user
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [Route("Notifications")]
        public NotificationsViewModel GetNotifications()
        {
            var userId = this.GetCurrentUserId();
            var notifsVm = new NotificationsViewModel();
            notifsVm.Soon = this.GetSoon();
            notifsVm.Live = this.GetLive();
            notifsVm.UnreadMessages = this._userServices.GetById(userId).UnreadMessages.Count;
            return notifsVm;
        }
    }
}