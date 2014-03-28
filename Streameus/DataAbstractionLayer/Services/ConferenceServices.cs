using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Streameus.DataAbstractionLayer.Contracts;
using Streameus.DataBaseAccess;
using Streameus.Exceptions;
using Streameus.Exceptions.HttpErrors;
using Streameus.Models;

namespace Streameus.DataAbstractionLayer.Services
{
    /// <summary>
    /// The conference services
    /// </summary>
    public class ConferenceServices : BaseServices<Conference>, IConferenceServices
    {
        private readonly IConferenceParametersServices _conferenceParametersServices;

        /// <summary>
        /// default constructor
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="conferenceParametersServices"></param>
        public ConferenceServices(IUnitOfWork unitOfWork, IConferenceParametersServices conferenceParametersServices)
            : base(unitOfWork)
        {
            this._conferenceParametersServices = conferenceParametersServices;
        }

        /// <summary>
        /// Save a conference
        /// </summary>
        /// <param name="conference"></param>
        protected override void Save(Conference conference)
        {
            if (conference.Id > 0)
                this.Update(conference);
            else
                this.Insert(conference);
            this.SaveChanges();
        }

        /// <summary>
        /// Delete a conference
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="NotFoundException"></exception>
        public new void Delete(int id)
        {
            var conference = this.GetById(id);
            if (conference == null)
                throw new NotFoundException("No such conference");
            var param = conference.ConferenceParameters;
            base.Delete(conference);
            this._conferenceParametersServices.Delete(param);
            this.SaveChanges();
        }

        /// <summary>
        /// Add a conference
        /// </summary>
        /// <param name="newConf"></param>
        public void AddConference(Conference newConf)
        {
            newConf.Id = 0;
            newConf.ConferenceParameters = new ConferenceParameters();
            this.Save(newConf);
        }

        /// <summary>
        /// Update a conference
        /// </summary>
        /// <param name="updatedConf"></param>
        public void UpdateConference(Conference updatedConf)
        {
            //Todo verifier que le owner id correspond a celui qui update.
            this.Save(updatedConf);
        }
    }
}