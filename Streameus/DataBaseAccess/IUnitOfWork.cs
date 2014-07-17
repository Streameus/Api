using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Streameus.DataBaseAccess
{
    /// <summary>
    /// Interface for the unitOfWork pattern
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Persists changes in db
        /// </summary>
        void SaveChanges();

        /// <summary>
        /// Get one of the entity set stored in the context
        /// </summary>
        /// <typeparam name="TEntity">The type of set to retrieve</typeparam>
        /// <returns>The entity type set</returns>
        IDbSet<TEntity> GetDbSet<TEntity>() where TEntity : class;

        /// <summary>
        /// Set a context's entry state
        /// </summary>
        /// <param name="entity">The entity</param>
        /// <param name="entityState">The state to set</param>
        /// <typeparam name="TEntity"></typeparam>
        void EntryState<TEntity>(TEntity entity, EntityState entityState) where TEntity : class;

        /// <summary>
        /// Get a context's entry state
        /// </summary>
        /// <param name="entity">The entity</param>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns>The entity state in the context</returns>
        EntityState EntryState<TEntity>(TEntity entity) where TEntity : class;
    }
}