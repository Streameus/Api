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
                context.Database.ExecuteSqlCommand(
                    "DECLARE @table_schema varchar(100) ,@table_name varchar(100) ,@constraint_schema varchar(100) ,@constraint_name varchar(100) ,@cmd nvarchar(200) DECLARE constraint_cursor CURSOR FOR select CONSTRAINT_SCHEMA, CONSTRAINT_NAME, TABLE_SCHEMA, TABLE_NAME from INFORMATION_SCHEMA.TABLE_CONSTRAINTS where TABLE_NAME != 'sysdiagrams' order by CONSTRAINT_TYPE asc  OPEN constraint_cursor FETCH NEXT FROM constraint_cursor INTO @constraint_schema, @constraint_name, @table_schema, @table_name WHILE @@FETCH_STATUS = 0  BEGIN SELECT @cmd = 'ALTER TABLE [' + @table_schema + '].[' + @table_name + '] DROP CONSTRAINT [' + @constraint_name + ']'     EXEC sp_executesql @cmd FETCH NEXT FROM constraint_cursor INTO @constraint_schema, @constraint_name, @table_schema, @table_name END CLOSE constraint_cursor DEALLOCATE constraint_cursor");
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