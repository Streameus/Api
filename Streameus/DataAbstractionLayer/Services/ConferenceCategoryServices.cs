using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Streameus.DataAbstractionLayer.Contracts;
using Streameus.DataBaseAccess;
using Streameus.Exceptions;
using Streameus.Exceptions.HttpErrors;
using Streameus.Models;

namespace Streameus.DataAbstractionLayer.Services
{
    /// <summary>
    /// The conference services
    /// </summary>
    public class ConferenceCategoryServices : BaseServices<ConferenceCategory>, IConferenceCategoryServices
    {
        /// <summary>
        /// default constructor
        /// </summary>
        /// <param name="unitOfWork"></param>
        public ConferenceCategoryServices(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        /// <summary>
        /// Save a conference
        /// </summary>
        /// <param name="category"></param>
        protected override void Save(ConferenceCategory category)
        {
            if (category.Id > 0)
                this.Update(category);
            else
                this.Insert(category);
            this.SaveChanges();
        }
    
    }
}