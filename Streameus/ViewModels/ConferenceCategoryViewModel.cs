using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Streameus.Models;

namespace Streameus.ViewModels
{
    /// <summary>
    /// A view model for the conference category
    /// </summary>
    public class ConferenceCategoryViewModel
    {
        /// <summary>
        /// Conference Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Conference name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Create a vm based on a conference category
        /// </summary>
        /// <param name="category"></param>
        public ConferenceCategoryViewModel(ConferenceCategory category)
        {
            this.Id = category.Id;
            this.Name = category.Name;
            this.Description = category.Description;
        }
    }
}