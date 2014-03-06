using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Streameus.DataAbstractionLayer.Contracts;
using Streameus.DataBaseAccess;
using Streameus.Models;

namespace Streameus.DataAbstractionLayer.Services
{
    public class ConferenceParametersServices : BaseServices<ConferenceParameters>, IConferenceParametersServices
    {
        public ConferenceParametersServices(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        protected override void Save(ConferenceParameters confParam)
        {
            if (confParam.Id > 0)
                this.Update(confParam);
            else
                this.Insert(confParam);
            this.SaveChanges();
        }

        public new void Delete(ConferenceParameters conferenceParameters)
        {
            base.Delete(conferenceParameters);
        }
    }
}