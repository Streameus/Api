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
    /// <summary>
    /// The base services for all services
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class BaseServices<TEntity> : IBaseServices<TEntity> where TEntity : class
    {
        /// <summary>
        /// Unit of work
        /// </summary>
        private readonly IUnitOfWork UnitOfWork;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="unitOfWork"></param>
        protected BaseServices(IUnitOfWork unitOfWork)
        {
            this.UnitOfWork = unitOfWork;
        }


        /// <summary>
        /// Default update method for an entity
        /// </summary>
        /// <param name="entityToUpdate"></param>
        protected virtual void Update(TEntity entityToUpdate)
        {
            this.UnitOfWork.GetDbSet<TEntity>().Attach(entityToUpdate);
            this.UnitOfWork.Context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        /// <summary>
        /// Default insert method for an entity
        /// </summary>
        /// <param name="entity"></param>
        protected virtual void Insert(TEntity entity)
        {
            this.UnitOfWork.GetDbSet<TEntity>().Add(entity);
        }

        /// <summary>
        /// Save the entity
        /// </summary>
        /// <param name="dataObject"></param>
        protected abstract void Save(TEntity dataObject);

        /// <summary>
        /// Get an entity by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        public TEntity GetById(int id)
        {
            var entity = this.GetDbSet<TEntity>().Find(id);
            if (entity == null)
                throw new NotFoundException("No such " + typeof (TEntity).Name);
            return entity;
        }

        /// <summary>
        /// Return all the Entity in a set
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TEntity> GetAll()
        {
            return this.UnitOfWork.GetDbSet<TEntity>();
        }

        /// <summary>
        /// Default method to delete an entity
        /// </summary>
        /// <remarks>No call to SaveChanges are made</remarks>
        /// <param name="id">id of the entity (entity will be fetched)</param>
        protected virtual void Delete(int id)
        {
            TEntity entityToDelete = this.UnitOfWork.GetDbSet<TEntity>().Find(id);
            this.UnitOfWork.GetDbSet<TEntity>().Remove(entityToDelete);
        }

        /// <summary>
        /// Default method to delete an entity
        /// </summary>
        /// <remarks>No call to SaveChanges are made</remarks>
        /// <param name="entityToDelete"></param>
        protected virtual void Delete(TEntity entityToDelete)
        {
            if (this.UnitOfWork.Context.Entry(entityToDelete).State == EntityState.Detached)
            {
                this.UnitOfWork.GetDbSet<TEntity>().Attach(entityToDelete);
            }
            this.UnitOfWork.GetDbSet<TEntity>().Remove(entityToDelete);
        }

        /// <summary>
        /// Return the entity dbSet
        /// </summary>
        /// <typeparam name="TRequestedEntity"></typeparam>
        /// <returns></returns>
        protected DbSet<TRequestedEntity> GetDbSet<TRequestedEntity>() where TRequestedEntity : class
        {
            return this.UnitOfWork.GetDbSet<TRequestedEntity>();
        }

        /// <summary>
        /// Persists changes in DataStore
        /// </summary>
        protected void SaveChanges()
        {
            this.UnitOfWork.SaveChanges();
        }
    }
}