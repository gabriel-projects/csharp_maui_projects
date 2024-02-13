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
    public class AppointmentRepository : IAppointmentRepository
    {
        public ApiDbContext ApiDbContext;

        public AppointmentRepository(ApiDbContext apiDbContext)
        {
            ApiDbContext = apiDbContext;
        }

        public async Task<List<IAppointment>> Appointmens()
        {
            var result = await ApiDbContext.Appointments.ToListAsync<IAppointment>();
            return result;
        }

        public async Task<IAppointment> Insert(IAppointment appointment)
        {
            if (appointment is not Appointment model) return null;

            ApiDbContext.Add(model);
            await ApiDbContext.SaveChangesAsync().ConfigureAwait(false);

           return model;
        }
    }
}
