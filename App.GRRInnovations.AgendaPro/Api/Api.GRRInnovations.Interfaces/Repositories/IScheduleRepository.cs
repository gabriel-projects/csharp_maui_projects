using Api.GRRInnovations.Interfaces.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.GRRInnovations.Interfaces.Repositories
{
    public interface IScheduleRepository
    {
        Task<ISchedule> Insert(ISchedule schedule);

        Task<List<ISchedule>> Schedules();
    }
}
