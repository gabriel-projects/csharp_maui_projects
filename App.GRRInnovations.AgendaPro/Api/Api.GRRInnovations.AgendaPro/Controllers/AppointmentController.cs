using Api.GRRInnovations.Domain.Models;
using Api.GRRInnovations.Domain.Wrappers;
using Api.GRRInnovations.Domain.Wrappers.In;
using Api.GRRInnovations.Domain.Wrappers.Out;
using Api.GRRInnovations.Infrastructure.Repositories;
using Api.GRRInnovations.Interfaces.Models;
using Api.GRRInnovations.Interfaces.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.GRRInnovations.AgendaPro.Controllers
{
    /// <summary>
    /// Controller responsavel por gerenciar os compromissos
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentRepository AppointmentRepository;

        public AppointmentController(IAppointmentRepository appointmentRepository)
        {
            AppointmentRepository = appointmentRepository;
        }

        [HttpPost("appointment/uid/{appointmentUid}/mark")]
        public async Task<ActionResult<WrapperOutAppointment>> AppointmentMark(Guid appointmentUid)
        {
            //todo: autorização

            var wp = await WrapperOutAppointment.From(new Appointment()).ConfigureAwait(false);
            return Ok(wp);
        }

        [HttpGet("appointments/available")]
        public async Task<ActionResult<List<WrapperOutAppointment>>> AppointmentAvailable()
        {
            //todo: obter os compromissos pelo jwt autorização

            var appointments = await AppointmentRepository.Appointments();

            var wp = await WrapperOutAppointment.From(appointments).ConfigureAwait(false);
            return Ok(wp);
        }
    }
}
