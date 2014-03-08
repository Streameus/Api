using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Streameus.Models;

namespace Streameus.DataAbstractionLayer.Contracts
{
    /// <summary>
    /// Interface for Conference Parameters services
    /// </summary>
    public interface IConferenceParametersServices : IBaseServices<ConferenceParameters>
    {
        /// <summary>
        /// Delete the conference parameters
        /// </summary>
        /// <remarks>No call to savechanges</remarks>
        /// <param name="conferenceParameters">the object to be deleted</param>
        void Delete(ConferenceParameters conferenceParameters);
    }
}