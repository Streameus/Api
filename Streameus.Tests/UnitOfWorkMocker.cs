using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Streameus.DataAbstractionLayer;
using Streameus.DataBaseAccess;

namespace Streameus.Tests
{
    /// <summary>
    /// This class allows to mock DbSets
    /// </summary>
    public class UnitOfWorkMocker
    {
        public Mock<StreameusContext> MockContext { get; set; }

        /// <summary>
        /// This is a real UnitOfWork, with a mocked StreameusContext
        /// </summary>
        public UnitOfWork UnitOfWork { get; set; }


        public UnitOfWorkMocker()
        {
            this.MockContext = new Mock<StreameusContext>();
            this.UnitOfWork = new UnitOfWork(this.MockContext.Object);
        }

        /// <summary>
        /// Add your fake DbSet to the context
        /// </summary>
        /// <typeparam name="TEntity">Entity DbSet type</typeparam>
        /// <param name="expression">The expression describing the dbset (ex: c => c.Users)</param>
        /// <param name="entityList">The fake dataList</param>
        public void AddFakeDbSet<TEntity>(Expression<Func<StreameusContext, IDbSet<TEntity>>> expression,
            IQueryable<TEntity> entityList) where TEntity : class
        {
            var mockSet = new Mock<DbSet<TEntity>>();
            mockSet.As<IQueryable<TEntity>>().Setup(m => m.Provider).Returns(entityList.Provider);
            mockSet.As<IQueryable<TEntity>>().Setup(m => m.Expression).Returns(entityList.Expression);
            mockSet.As<IQueryable<TEntity>>().Setup(m => m.ElementType).Returns(entityList.ElementType);
            mockSet.As<IQueryable<TEntity>>().Setup(m => m.GetEnumerator()).Returns(entityList.GetEnumerator());
            this.MockContext.Setup(c => c.GetDbSet<TEntity>()).Returns(mockSet.Object);
            this.MockContext.Setup(expression).Returns(mockSet.Object);
        }
    }
}