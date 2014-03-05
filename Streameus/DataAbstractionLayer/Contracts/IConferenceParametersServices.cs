using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Streameus.Models;

namespace Streameus.DataAbstractionLayer.Contracts
{
    public interface IConferenceParametersServices : IBaseServices<ConferenceParameters>
    {
        void Delete(ConferenceParameters conferenceParameters);
    }
}