using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using Streameus.DataAbstractionLayer;

namespace Streameus.DataBaseAccess
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly DbContext _context;
        private bool disposed = false;

        public UnitOfWork(StreameusContainer context)
        {
            _context = context;
        }

        public void SaveChanges()
        {
            if (_context != null)
            {
                _context.SaveChanges();
            }
        }

        public DbContext Context
        {
            get { return _context; }
        }

        public DbSet<TEntity> GetDbSet<TEntity>() where TEntity : class
        {
            return this.Context.Set<TEntity>();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}