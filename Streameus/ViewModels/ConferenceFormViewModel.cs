using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Streameus.Models;

namespace Streameus.ViewModels
{
    public class ConferenceFormViewModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime? Time { get; set; }

        /// <summary>
        /// Time in minutes
        /// </summary>
        [Required]
        [Range(15, 240)]
        public int ScheduledDuration { get; set; }
    }
}