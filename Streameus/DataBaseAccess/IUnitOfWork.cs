using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Streameus.DataBaseAccess
{
    public interface IUnitOfWork
    {
        void SaveChanges();
        DbContext Context { get; }

        DbSet<TEntity> GetDbSet<TEntity>() where TEntity : class;
    }
}