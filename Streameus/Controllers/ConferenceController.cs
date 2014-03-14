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
    /// Conference controller
    /// </summary>
    public class ConferenceController : BaseController
    {
        private readonly IConferenceServices _conferenceServices;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="conferenceServices"></param>
        public ConferenceController(IConferenceServices conferenceServices)
        {
            this._conferenceServices = conferenceServices;
        }

        // GET api/conference
        /// <summary>
        /// Retourne toutes les conferences
        /// </summary>
        /// <returns></returns>
        /// <responseCode></responseCode>
        public IEnumerable<ConferenceViewModel> Get()
        {
            var conferences = new List<ConferenceViewModel>();
            this._conferenceServices.GetAll().ForEach(c => conferences.Add(new ConferenceViewModel(c)));
            return conferences;
        }

        // GET api/conference/5
        /// <summary>
        /// Get one conference
        /// </summary>
        /// <param name="id">the id of the conference</param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        public ConferenceViewModel Get(int id)
        {
            var conf = this._conferenceServices.GetById(id);
            if (conf == null)
                throw new NotFoundException(Translation.ConferenceNotFound);
            ConferenceViewModel conference = new ConferenceViewModel(conf);
            return conference;
        }

        // POST api/conference
        /// <summary>
        /// Create a new conference
        /// </summary>
        /// <param name="conference">the infos for the new conference</param>
        /// <returns></returns>
        public ConferenceViewModel Post([FromBody] ConferenceFormViewModel conference)
        {
            var newConf = new Conference()
            {
                Name = conference.Name,
                Description = conference.Description,
                ScheduledDuration = conference.ScheduledDuration,
                Time = conference.Time.Value,
                Status = DataBaseEnums.ConfStatus.AVenir,
                OwnerId = 1 //TODO changer une fois l'Auth implementee.
            };
            this._conferenceServices.AddConference(newConf);
            return this.Get(newConf.Id);
        }

        // PUT api/conference/5
        /// <summary>
        /// Update a conference
        /// </summary>
        /// <param name="conference"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        public ConferenceViewModel Put([FromBody] ConferenceFormViewModel conference)
        {
            var updatedConf = this._conferenceServices.GetById(conference.Id);
            if (updatedConf == null)
                throw new NotFoundException("Conference not found");
            else
            {
                updatedConf.Name = conference.Name;
                updatedConf.Description = conference.Description;
                updatedConf.ScheduledDuration = conference.ScheduledDuration;
                updatedConf.Time = conference.Time.Value;
                this._conferenceServices.UpdateConference(updatedConf);
            }
            return this.Get(conference.Id);
        }

        // DELETE api/conference/5
        /// <summary>
        /// Delete a conference
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            this._conferenceServices.Delete(id);
        }
    }
}