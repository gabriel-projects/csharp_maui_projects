using Api.GRRInnovations.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Api.GRRInnovations.Domain.Models
{
    public class Schedule : BaseModel, ISchedule
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsAllDay { get; set; }
        public string Location { get; set; }
        public string Subject { get; set; }

        public List<ScheduledAppointment>? DbAppointments { get; set; }

        public List<IScheduledAppointment>? Appointments
        {
            get => DbAppointments?.Cast<IScheduledAppointment>()?.ToList();
            set => DbAppointments = value?.Cast<ScheduledAppointment>()?.ToList();
        }
        public string Name { get; set; }

        public Schedule()
        {
            Appointments = new List<IScheduledAppointment>();
        }
    }
}
