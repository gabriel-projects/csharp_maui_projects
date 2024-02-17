using Api.GRRInnovations.Domain.Models;
using Api.GRRInnovations.Infrastructure.Context;
using Api.GRRInnovations.Interfaces.Models;
using Api.GRRInnovations.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.GRRInnovations.Infrastructure.Repositories
{
    public class ScheduleRepository : IScheduleRepository
    {
        public ApiDbContext ApiDbContext;

        public ScheduleRepository(ApiDbContext apiDbContext)
        {
            ApiDbContext = apiDbContext;
        }

        public async Task<List<ISchedule>> Schedules()
        {
            var result = await ApiDbContext.Schedules.ToListAsync<ISchedule>();
            return result;
        }

        public async Task<ISchedule> Insert(ISchedule schedule)
        {
            if (schedule is not Schedule model) return null;

            ApiDbContext.Add(model);
            await ApiDbContext.SaveChangesAsync().ConfigureAwait(false);

           return model;
        }
    }
}
