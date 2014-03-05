using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Streameus.Models;

namespace Streameus.DataAbstractionLayer.Contracts
{
    public interface IConferenceServices : IBaseServices<Conference>
    {
        void Delete(int id);
        void AddConference(Conference newConf);
        void UpdateConference(Conference updatedConf);
    }
}