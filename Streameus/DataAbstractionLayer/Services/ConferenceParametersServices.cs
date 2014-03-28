using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Streameus.DataAbstractionLayer.Contracts;
using Streameus.DataBaseAccess;
using Streameus.Models;

namespace Streameus.DataAbstractionLayer.Services
{
    /// <summary>
    /// Conference parameters services
    /// </summary>
    public class ConferenceParametersServices : BaseServices<ConferenceParameters>, IConferenceParametersServices
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="unitOfWork"></param>
        public ConferenceParametersServices(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        /// <summary>
        /// Save the entity
        /// </summary>
        /// <param name="confParam">Entity</param>
        protected override void Save(ConferenceParameters confParam)
        {
//            if (confParam.Id > 0)
//                this.Update(confParam);
//            else
//                this.Insert(confParam);
//            this.SaveChanges();
        }

        /// <summary>
        /// Delete Conference parameters
        /// </summary>
        /// <remarks>No call to SaveChanges</remarks>
        /// <param name="conferenceParameters"></param>
        public new void Delete(ConferenceParameters conferenceParameters)
        {
//            base.Delete(conferenceParameters);
        }
    }
}