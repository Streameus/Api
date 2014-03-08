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
        /// Get the context used within the unit of work
        /// </summary>
        DbContext Context { get; }

        /// <summary>
        /// Get one of the entity set stored in the context
        /// </summary>
        /// <typeparam name="TEntity">The type of set to retrieve</typeparam>
        /// <returns>The entity type set</returns>
        DbSet<TEntity> GetDbSet<TEntity>() where TEntity : class;
    }
}