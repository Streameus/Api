using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Streameus.DataAbstractionLayer.Initializers
{
    /// <summary>
    /// This class is used to initialize Streameus Db with appharbor's constraints
    /// </summary>
    public class StreameusInitializerForAppHarbor : DropCreateDatabaseTables<StreameusContainer>
    {
        /// <summary>
        /// Call the default seed method <see cref="StreameusSeeder"/>
        /// </summary>
        /// <param name="context"></param>
        protected override void Seed(StreameusContainer context)
        {
            StreameusSeeder.Seed(context);
        }
    }
}