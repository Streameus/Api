using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Streameus.Models;

namespace Streameus.ViewModels
{
    public class ConferenceViewModel
    {
        public int Id { get; set; }
        public int Owner { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Streameus.Enums.DataBaseEnums.ConfStatus Status { get; set; }
        public System.DateTime Time { get; set; }
        public int ScheduledDuration { get; set; }
        public int FinalDuration { get; set; }

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
        }
    }
}