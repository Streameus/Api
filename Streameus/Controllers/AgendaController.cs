using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.OData.Query;
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
        /// Retourne toutes les conferences EN COURS et A VENIR auxquelles l'utilisateur a souscrit
        /// </summary>
        /// <returns>Retourne une List Conf (int Id, DateTime Time, String Name) ou une List vide si null</returns>
        /// <responseCode></responseCode>
        [Authorize]
        public IOrderedEnumerable<KeyValuePair<DateTime, List<ConferenceAgendaViewModel>>> Get(ODataQueryOptions<Conference> options = null)
        {
            var owner = this._userServices.GetById(this.GetCurrentUserId());
            var conferences = owner.ConferencesRegistered.Concat(owner.ConferencesCreated)
                .Concat(owner.ConferencesInvolved)
                .Where(c => c.Status == DataBaseEnums.ConfStatus.AVenir || c.Status == DataBaseEnums.ConfStatus.EnCours)
                .OrderBy(c => c.Time);

            var confList = new List<ConferenceAgendaViewModel>();
            if (options != null)
            {
                var confs = conferences.AsQueryable();
                confs = options.ApplyTo(confs) as IQueryable<Conference>;
                if (confs != null)
                    foreach (var conference in confs)
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
            }
            else
            {
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
            }
            var conflistDay = new Dictionary<DateTime, List<ConferenceAgendaViewModel>>();
            foreach (var conferenceAgendaViewModel in confList)
            {
                if (!conflistDay.ContainsKey(conferenceAgendaViewModel.Date))
                    conflistDay.Add(conferenceAgendaViewModel.Date,
                        new List<ConferenceAgendaViewModel>() { conferenceAgendaViewModel });
                else
                {
                    conflistDay[conferenceAgendaViewModel.Date].Add(conferenceAgendaViewModel);
                }
            }
            return conflistDay.OrderBy(d => d.Key);
        }

        /// <summary>
        /// Get all the ongoing conferences the user suscribed to
        /// </summary>
        /// <returns></returns>
        [Route("Live")]
        [Authorize]
        public IEnumerable<ConferenceAgendaViewModel> GetLive(ODataQueryOptions<Conference> options = null)
        {
            var conferences = this._conferenceServices.GetLiveConferenceForUser(this.GetCurrentUserId());

            var conferencesList = new List<ConferenceAgendaViewModel>();
            if (options != null)
                conferences = options.ApplyTo(conferences.AsQueryable()) as IQueryable<Conference>;
            conferences.ForEach(c => conferencesList.Add(new ConferenceAgendaViewModel() { Date = c.Time, Id = c.Id, Name = c.Name }));
            return conferencesList;
        }

        /// <summary>
        /// Get all conference the user suscribed to airing in the next 24 hours
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [Route("Soon")]
        public IEnumerable<ConferenceAgendaViewModel> GetSoon(ODataQueryOptions<Conference> options = null)
        {
            var conferences = this._conferenceServices.GetSoonConferenceForUser(this.GetCurrentUserId());

            var conferencesList = new List<ConferenceAgendaViewModel>();
            if (options != null)
                conferences = options.ApplyTo(conferences.AsQueryable()) as IQueryable<Conference>;
            conferences.ForEach(c => conferencesList.Add(new ConferenceAgendaViewModel() { Date = c.Time, Id = c.Id, Name = c.Name }));
            return conferencesList;
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