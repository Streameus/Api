using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Streameus.DataAbstractionLayer.Contracts;
using Streameus.DataBaseAccess;
using Streameus.Models;

namespace Streameus.DataAbstractionLayer.Services
{
    public class ParametersServices : BaseServices<Parameters>, IParametersServices
    {
        public ParametersServices(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        protected override void Save(Parameters parameters)
        {
            if (parameters.Id > 0)
                this.Update(parameters);
            else
                this.Insert(parameters);
            this.SaveChanges();
        }

        /// <summary>
        /// Efface un parametre.
        /// </summary>
        /// <param name="parameters"></param>
        public new void Delete(Parameters parameters)
        {
            base.Delete(parameters);
            this.SaveChanges();
        }
    }
}