using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Streameus.Models;

namespace Streameus.ViewModels
{
    /// <summary>
    /// A view model for the conference
    /// </summary>
    public class ConferenceViewModel
    {
        /// <summary>
        /// Conference Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Id of the user whho owns the conf
        /// </summary>
        public int Owner { get; set; }

        /// <summary>
        /// Conference name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Status of the conference
        /// </summary>
        public Streameus.Enums.DataBaseEnums.ConfStatus Status { get; set; }

        /// <summary>
        /// The time the conference is/was scheduled
        /// </summary>
        public System.DateTime Time { get; set; }

        /// <summary>
        /// The conference's scheduled duration
        /// </summary>
        public int ScheduledDuration { get; set; }

        /// <summary>
        /// The conference final duration
        /// </summary>
        public int FinalDuration { get; set; }

        /// <summary>
        /// The conference category
        /// </summary>
        public ConferenceCategoryViewModel Category { get; set; }

        /// <summary>
        /// Id of the room for streaming
        /// </summary>
        public string RoomId { get; set; }

        /// <summary>
        /// Entrance fee to the conference.
        /// </summary>
        public double EntranceFee { get; set; }

        /// <summary>
        /// Average mark given by users
        /// </summary>
        public double? Mark { get; set; }

        /// <summary>
        /// Create a vm based on a conference
        /// </summary>
        /// <param name="conf"></param>
        public ConferenceViewModel(Conference conf)
        {
            this.Id = conf.Id;
            this.Owner = conf.OwnerId;
            this.Name = conf.Name;

            this.Description = conf.Description;
            this.Status = conf.Status;
            this.Time = conf.Time;
            this.ScheduledDuration = conf.ScheduledDuration;
            this.FinalDuration = conf.FinalDuration;
            this.Category = new ConferenceCategoryViewModel(conf.Category);
            this.RoomId = conf.RoomId;
            this.EntranceFee = conf.EntranceFee;

            //Check for false marks
            if (conf.Mark > -1)
            {
                if (Math.Abs(conf.Mark) < 0.1 && !conf.Marks.Any())
                {
                    this.Mark = null;
                }
                else
                {
                    this.Mark = conf.Mark;
                }
            }
        }
    }
}