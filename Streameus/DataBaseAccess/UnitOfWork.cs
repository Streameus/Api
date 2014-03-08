using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using Streameus.DataAbstractionLayer;

namespace Streameus.DataBaseAccess
{
    /// <summary>
    /// The unit of work implementation
    /// </summary>
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly DbContext _context;
        private bool _disposed = false;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="context">The context to be used </param>
        public UnitOfWork(StreameusContainer context)
        {
            _context = context;
        }

        /// <summary>
        /// Persists changes to DataStore
        /// </summary>
        public void SaveChanges()
        {
            if (_context != null)
            {
                _context.SaveChanges();
            }
        }

        /// <summary>
        /// Get the DbContext uset in the UnitOfWork
        /// </summary>
        public DbContext Context
        {
            get { return _context; }
        }

        /// <summary>
        /// Return an entity's set
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public DbSet<TEntity> GetDbSet<TEntity>() where TEntity : class
        {
            return this.Context.Set<TEntity>();
        }

        /// <summary>
        /// Dispose of the resources
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this._disposed = true;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}