using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Streameus.Models;

namespace Streameus.DataAbstractionLayer.Initializers
{
    /// <summary>
    /// This is the default class used to initialize streameus
    /// </summary>
    public class StreameusInitializer : DropCreateDatabaseAlways<StreameusContainer>
    {
        /// <summary>
        /// A method that should be overridden to actually add data to the context for seeding.
        ///             The default implementation does nothing.
        /// </summary>
        /// <param name="context">The context to seed. </param>
        protected override void Seed(StreameusContainer context)
        {
            StreameusSeeder.Seed(context);
        }
    }
}