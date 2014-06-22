﻿using System;
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
        /// Class returned as a list of conferences to the client
        /// </summary>
        public class Conf
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
        public IEnumerable<Conf> Get()
        {
            var owner = this._userServices.GetAll().First(); //TODO changer une fois l'Auth implementee.
            var conferences = owner.ConferencesRegistered.OrderBy(c => c.Time);
            var confList = new List<Conf>();
            foreach (var conference in conferences)
            {
                if (conference.Status != DataBaseEnums.ConfStatus.Finie)
                {
                    var confInfo = new Conf
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
        public IEnumerable<Conf> Post(int id)
        {
            var conf = this._conferenceServices.GetById(id);
            if (conf == null)
                throw new NotFoundException(Translation.ConferenceNotFound);
            var participant = this._userServices.GetAll().First(); //TODO changer une fois l'Auth implementee.
            if (conf.Status == DataBaseEnums.ConfStatus.Finie)
            {
               // throw new Exception("You can not subscribe to a conference in the past.");
                throw new ForbiddenException("Je te laisse faire la trad thai ;)"); //TODO
            }
            else
            {
                participant.ConferencesRegistered.Add(conf);
                this._userServices.UpdateUser(participant);
                return this.Get();
            }
        }

    }
}
