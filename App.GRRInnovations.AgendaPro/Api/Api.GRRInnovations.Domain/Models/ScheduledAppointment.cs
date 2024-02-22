using Api.GRRInnovations.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.GRRInnovations.Domain.Models
{
    public class ScheduledAppointment : BaseModel<Schedule, ISchedule>, IScheduledAppointment
    {
    }
}
