using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Streameus.DataAbstractionLayer.Contracts;
using Streameus.DataBaseAccess;
using Streameus.Exceptions;
using Streameus.Models;

namespace Streameus.DataAbstractionLayer.Services
{
    public class ConferenceServices : BaseServices<Conference>, IConferenceServices
    {
        private readonly IConferenceParametersServices _conferenceParametersServices;

        public ConferenceServices(IUnitOfWork unitOfWork, IConferenceParametersServices conferenceParametersServices)
            : base(unitOfWork)
        {
            this._conferenceParametersServices = conferenceParametersServices;
        }

        protected override void Save(Conference conference)
        {
            if (conference.Id > 0)
                this.Update(conference);
            else
                this.Insert(conference);
            this.SaveChanges();
        }

        public new void Delete(int id)
        {
            var conference = this.GetById(id);
            if (conference == null)
                HttpErrors.NotFound("No such conference");
            var param = conference.ConferenceParameter;
            base.Delete(conference);
            this._conferenceParametersServices.Delete(param);
            this.SaveChanges();
        }

        public void AddConference(Conference newConf)
        {
            newConf.Id = 0;
            newConf.ConferenceParameter = new ConferenceParameters();
            this.Save(newConf);
        }

        public void UpdateConference(Conference updatedConf)
        {
            //Todo verifier que le owner id correspond a celui qui update.
            this.Save(updatedConf);
        }
    }
}