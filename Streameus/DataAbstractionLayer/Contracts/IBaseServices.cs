using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Streameus.Exceptions.HttpErrors;
using Streameus.ViewModels;

namespace Streameus.DataAbstractionLayer.Contracts
{
    /// <summary>
    /// Base services interface for every entity
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    public interface IBaseServices<TEntity> where TEntity : class
    {
        /// <summary>
        /// Get an entity by it's id
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="NotFoundException"></exception>
        /// <returns></returns>
        TEntity GetById(int id);

        /// <summary>
        /// Return all existing entry for this entity
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> GetAll();
    }
}