using Api.GRRInnovations.Domain.Models;
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
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentRepository AppointmentsRepository;

        public AppointmentsController(IAppointmentRepository appointmentsRepository)
        {
            AppointmentsRepository = appointmentsRepository;
        }

        [HttpPost("schedule")]
        public async Task<ActionResult<WrapperOutAppointment>> CreateSchedule([FromBody] WrapperInAppointment<Appointment> wrapperInAppointment)
        {
            var appointment = await wrapperInAppointment.Result();

            await AppointmentsRepository.Insert(appointment);


            var wp = await WrapperOutAppointment.From(new Appointment()).ConfigureAwait(false);
            return Ok(wp);
        }
    }
}
