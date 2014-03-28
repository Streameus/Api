using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Streameus.Models;

namespace Streameus.DataAbstractionLayer.Initializers
{
    /// <summary>
    /// This class is used to initialize Streameus Db with appharbor's constraints
    /// </summary>
    public class StreameusInitializerForAppHarbor : DropCreateDatabaseTables<StreameusContext>
    {
        /// <summary>
        /// Call the default seed method <see cref="StreameusSeeder"/>
        /// </summary>
        /// <param name="context"></param>
        protected override void Seed(StreameusContext context)
        {
            StreameusSeeder.Seed(context);
        }
    }
}