using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Streameus.Models;

namespace Streameus.DataAbstractionLayer.Contracts
{
    /// <summary>
    /// Interface for the parameters services
    /// </summary>
    public interface IParametersServices : IBaseServices<Parameters>
    {
        /// <summary>
        /// Delete parameters
        /// </summary>
        /// <param name="parameters"></param>
        void Delete(Parameters parameters);
    }
}