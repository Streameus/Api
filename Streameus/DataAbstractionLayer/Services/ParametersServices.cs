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
    /// Parameters services
    /// </summary>
    public class ParametersServices : BaseServices<Parameters>, IParametersServices
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="unitOfWork">The unit of work</param>
        public ParametersServices(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        /// <summary>
        /// Save the parameters
        /// </summary>
        /// <param name="parameters"></param>
        protected override void Save(Parameters parameters)
        {
        }

        /// <summary>
        /// Efface un parametre.
        /// </summary>
        /// <param name="parameters"></param>
        public new void Delete(Parameters parameters)
        {
        }
    }
}