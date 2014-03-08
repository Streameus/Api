using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Streameus.Models;

namespace Streameus.ViewModels
{
    /// <summary>
    /// Conference View Model used for creation and modification
    /// </summary>
    public class ConferenceFormViewModel
    {
        /// <summary>
        /// Conference Id
        /// </summary>
        public int Id { get; set; }


        /// <summary>
        /// Conference name
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// Conference description
        /// </summary>
        [Required]
        public string Description { get; set; }

        /// <summary>
        /// Conference scheduled time
        /// </summary>
        [Required]
        public DateTime? Time { get; set; }

        /// <summary>
        /// Time in minutes for the conference scheduled duration
        /// </summary>
        [Required]
        [Range(15, 240)]
        public int ScheduledDuration { get; set; }
    }
}