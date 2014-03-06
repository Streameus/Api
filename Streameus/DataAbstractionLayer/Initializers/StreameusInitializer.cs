using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Streameus.Models;

namespace Streameus.DataAbstractionLayer.Initializers
{
    public class StreameusInitializer : DropCreateDatabaseAlways<StreameusContainer>
    {
        protected override void Seed(StreameusContainer context)
        {
            StreameusSeeder.Seed(context);
        }
    }
}