using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Streameus.Models;

namespace Streameus.DataAbstractionLayer.Contracts
{
    /// <summary>
    /// Interface for the conference services
    /// </summary>
    public interface IConferenceServices : IBaseServices<Conference>
    {
        /// <summary>
        /// Delete a conference
        /// </summary>
        /// <param name="id">The id of the conference</param>
        void Delete(int id);

        /// <summary>
        /// Add a new conference
        /// </summary>
        /// <param name="newConf">The conference to be added</param>
        void AddConference(Conference newConf);

        /// <summary>
        /// Updates a conference
        /// </summary>
        /// <param name="updatedConf"></param>
        void UpdateConference(Conference updatedConf);
    }
}