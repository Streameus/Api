﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Streameus.DataAbstractionLayer.Contracts;
using Streameus.Enums;
using Streameus.Exceptions;
using Streameus.Models;
using Streameus.ViewModels;
using WebGrease.Css.Extensions;

namespace Streameus.Controllers
{
    public class ConferenceController : BaseController
    {
        private readonly IConferenceServices _conferenceServices;

        public ConferenceController(IConferenceServices conferenceServices)
        {
            this._conferenceServices = conferenceServices;
        }

        // GET api/conference
        public IEnumerable<ConferenceViewModel> Get()
        {
            var conferences = new List<ConferenceViewModel>();
            this._conferenceServices.GetAll().ForEach(c => conferences.Add(new ConferenceViewModel(c)));
            return conferences;
        }

        // GET api/conference/5
        public ConferenceViewModel Get(int id)
        {
            ConferenceViewModel conference;
            var conf = this._conferenceServices.GetById(id);
            if (conf == null)
                HttpErrors.NotFound("User not found!");
            conference = new ConferenceViewModel(conf);
            return conference;
        }

        // POST api/conference
        //Utiliser un VM different pour pouvoir selectionner les champs obligatoires.
        public ConferenceViewModel Post([FromBody] ConferenceFormViewModel conference)
        {
            if (!ModelState.IsValid)
            {
                HttpErrors.ValidationError(ModelState);
            }
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
        public ConferenceViewModel Put([FromBody] ConferenceFormViewModel conference)
        {
            if (!ModelState.IsValid)
                HttpErrors.ValidationError(ModelState);
            var updatedConf = this._conferenceServices.GetById(conference.Id);
            if (updatedConf == null)
                HttpErrors.NotFound("Conference not found");
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
        public void Delete(int id)
        {
            this._conferenceServices.Delete(id);
        }
    }
}