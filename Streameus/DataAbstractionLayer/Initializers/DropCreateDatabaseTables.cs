using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Transactions;

namespace Streameus.DataAbstractionLayer.Initializers
{
    /// <summary>
    /// Ne marche que sur SQLSERVER!!
    /// Cette classe permet de recreer les tables d'une DB sans drop la table.
    /// Pratique en cas de droits limites. 
    /// Le seed par defaut ne fait rien
    /// </summary>
    /// <typeparam name="TContext"></typeparam>
    public class DropCreateDatabaseTables<TContext> : IDatabaseInitializer<TContext>
        where TContext : global::System.Data.Entity.DbContext
    {
        #region IDatabaseInitializer<Context> Members

        public void InitializeDatabase(TContext context)
        {
            bool dbExists;
            using (new TransactionScope(TransactionScopeOption.Suppress))
            {
                dbExists = context.Database.Exists();
            }
            if (dbExists)
            {
                // remove all tables
                context.Database.ExecuteSqlCommand("EXEC sp_MSforeachtable @command1 = \"DROP TABLE ?\"");

                // create all tables
                var dbCreationScript = ((IObjectContextAdapter) context).ObjectContext.CreateDatabaseScript();
                context.Database.ExecuteSqlCommand(dbCreationScript);

                this.Seed(context);
                context.SaveChanges();
            }
            else
            {
                throw new ApplicationException("No database instance");
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Seed the DB, default does nothing
        /// </summary>
        /// <param name="context">DBContext currently used</param>
        protected virtual void Seed(TContext context)
        {
        }

        #endregion
    }
}