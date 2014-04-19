using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
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
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="unitOfWork"></param>
        protected BaseServices(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }


        /// <summary>
        /// Default update method for an entity
        /// </summary>
        /// <param name="entityToUpdate"></param>
        protected virtual void Update(TEntity entityToUpdate)
        {
            this._unitOfWork.GetDbSet<TEntity>().Attach(entityToUpdate);
            this._unitOfWork.EntryState(entityToUpdate, EntityState.Modified);
        }

        /// <summary>
        /// Default insert method for an entity
        /// </summary>
        /// <param name="entity"></param>
        protected virtual void Insert(TEntity entity)
        {
            this._unitOfWork.GetDbSet<TEntity>().Add(entity);
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
        public virtual TEntity GetById(int id)
        {
            //DO NOT EVER DO THAT
            var entity = this.GetDbSet<TEntity>().Where("Id ==" + id).FirstOrDefault();
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
            return this.GetDbSet<TEntity>();
        }

        /// <summary>
        /// Default method to delete an entity
        /// </summary>
        /// <remarks>No call to SaveChanges are made</remarks>
        /// <param name="id">id of the entity (entity will be fetched)</param>
        protected virtual void Delete(int id)
        {
            TEntity entityToDelete = this.GetDbSet<TEntity>().Find(id);
            this._unitOfWork.GetDbSet<TEntity>().Remove(entityToDelete);
        }

        /// <summary>
        /// Default method to delete an entity
        /// </summary>
        /// <remarks>No call to SaveChanges are made</remarks>
        /// <param name="entityToDelete"></param>
        protected virtual void Delete(TEntity entityToDelete)
        {
            if (this._unitOfWork.EntryState(entityToDelete) == EntityState.Detached)
            {
                this._unitOfWork.GetDbSet<TEntity>().Attach(entityToDelete);
            }
            this._unitOfWork.GetDbSet<TEntity>().Remove(entityToDelete);
        }

        /// <summary>
        /// Return the entity dbSet
        /// </summary>
        /// <typeparam name="TRequestedEntity"></typeparam>
        /// <returns></returns>
        protected DbSet<TRequestedEntity> GetDbSet<TRequestedEntity>() where TRequestedEntity : class
        {
            return this._unitOfWork.GetDbSet<TRequestedEntity>();
        }

        /// <summary>
        /// Persists changes in DataStore
        /// </summary>
        protected void SaveChanges()
        {
            this._unitOfWork.SaveChanges();
        }
    }
}