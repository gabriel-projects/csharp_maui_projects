using Api.GRRInnovations.Interfaces.Models;
using System;

namespace Api.GRRInnovations.Domain.Models
{
    public class Appointment : BaseModel, IAppointment
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsAllDay { get; set; }
        public string Location { get; set; }
        public string Subject { get; set; }
    }
}
