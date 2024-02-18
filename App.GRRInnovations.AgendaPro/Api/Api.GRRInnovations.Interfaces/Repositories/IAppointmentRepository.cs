using Api.GRRInnovations.Interfaces.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.GRRInnovations.Interfaces.Repositories
{
    public interface IAppointmentRepository
    {
        Task<IAppointment> Insert(IAppointment appointment);

        Task<List<IAppointment>> Appointments();
    }
}
