using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Streameus.DataAbstractionLayer.Contracts;
using Streameus.DataBaseAccess;
using Streameus.Exceptions;
using Streameus.Exceptions.HttpErrors;
using Streameus.ViewModels;

namespace Streameus.DataAbstractionLayer.Services
{
    public abstract class BaseServices<TEntity> : IBaseServices<TEntity> where TEntity : class
    {
        protected IUnitOfWork unitOfWork;

        protected BaseServices(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }


        protected virtual void Update(TEntity entityToUpdate)
        {
            this.unitOfWork.GetDbSet<TEntity>().Attach(entityToUpdate);
            this.unitOfWork.Context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        protected virtual void Insert(TEntity entity)
        {
            this.unitOfWork.GetDbSet<TEntity>().Add(entity);
        }

        protected abstract void Save(TEntity dataObject);

        public TEntity GetById(int id)
        {
            var entity = this.GetDbSet<TEntity>().Find(id);
            if (entity == null)
                throw new NotFoundException("No such " + typeof (TEntity).Name);
            return entity;
        }

        public IEnumerable<TEntity> GetAll()
        {
            return this.unitOfWork.GetDbSet<TEntity>();
        }

        protected virtual void Delete(int id)
        {
            TEntity entityToDelete = this.unitOfWork.GetDbSet<TEntity>().Find(id);
            this.unitOfWork.GetDbSet<TEntity>().Remove(entityToDelete);
        }

        protected virtual void Delete(TEntity entityToDelete)
        {
            if (this.unitOfWork.Context.Entry(entityToDelete).State == EntityState.Detached)
            {
                this.unitOfWork.GetDbSet<TEntity>().Attach(entityToDelete);
            }
            this.unitOfWork.GetDbSet<TEntity>().Remove(entityToDelete);
        }

        protected DbSet<TRequestedEntity> GetDbSet<TRequestedEntity>() where TRequestedEntity : class
        {
            return this.unitOfWork.GetDbSet<TRequestedEntity>();
        }

        protected void SaveChanges()
        {
            this.unitOfWork.SaveChanges();
        }
    }
}