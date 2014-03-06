using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Streameus.DataAbstractionLayer.Initializers
{
    public class StreameusInitializerForAppHarbor : DropCreateDatabaseTables<StreameusContainer>
    {
        protected override void Seed(StreameusContainer context)
        {
            StreameusSeeder.Seed(context);
        }
    }
}