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

        // POST api/agenda/subscribe/5
        /// <summary>
        /// Souscription a une conference
        /// </summary>
        /// <param name="id">Conference ID</param>
        /// <returns></returns>
        /// <responseCode></responseCode>
        /// <exception cref="NotFoundException">Conference not found</exception>
        /// <exception cref="ForbiddenException">Conference is already done</exception>
        [Route("subscribe/{id}")]
        [Authorize]
        public IEnumerable<ConferenceAgendaViewModel> Post(int id)
        {
            var conf = this._conferenceServices.GetById(id);
            if (conf == null)
                throw new NotFoundException(Translation.ConferenceNotFound);
            var participant = this._userServices.GetById(this.GetCurrentUserId());
            if (conf.Status == DataBaseEnums.ConfStatus.Finie)
                throw new ForbiddenException(Translation.ErrorSuscribePastConference);
            participant.ConferencesRegistered.Add(conf);
            this._userServices.UpdateUser(participant);
            return this.Get();
        }

        [Route("live")]
        public IEnumerable<ConferenceAgendaViewModel> GetLive(int userId)
        {
            var conferences = this._conferenceServices.GetLiveConferenceForUser(userId);
            List<ConferenceAgendaViewModel> liveConferences = new List<ConferenceAgendaViewModel>();
            var test = conferences.Select(c => new ConferenceAgendaViewModel() {Date = c.Time, Id = c.Id, Name = c.Name});

//            conferences.ForEach(c => liveConferences.Add(new ConferenceAgendaViewModel(c)));
            return test;
        }
    }
}