using Api.GRRInnovations.Interfaces.Models;
using System.Threading.Tasks;

namespace Api.GRRInnovations.Interfaces.Repositories
{
    public interface IAppointmentRepository
    {
        Task<IAppointment> Insert(IAppointment appointment);
    }
}
