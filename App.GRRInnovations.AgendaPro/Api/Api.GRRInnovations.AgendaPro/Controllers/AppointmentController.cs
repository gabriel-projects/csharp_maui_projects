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

        [HttpPost("schedule")]
        public async Task<ActionResult<WrapperOutAppointment>> CreateSchedule([FromBody] WrapperInAppointment<IAppointment> wrapperInAppointment)
        {
            //todo: autorização
            var appointment = await wrapperInAppointment.Result();

            appointment = await AppointmentRepository.Insert(appointment);

            //todo: obter melhor mensagem para tratar o erro
            if (appointment == null) return new NotFoundObjectResult(new WrapperError { Message = "Falha ao criar apontamento, tente novamente." });

            var wp = await WrapperOutAppointment.From(new Appointment()).ConfigureAwait(false);
            return Ok(wp);
        }

        [HttpGet("schedules")]
        public async Task<ActionResult<List<WrapperOutAppointment>>> Schedules()
        {
            //todo: obter os compromissos pelo jwt autorização

            var appointments = await AppointmentRepository.Appointmens();

            var wp = await WrapperOutAppointment.From(appointments).ConfigureAwait(false);
            return Ok(wp);
        }
    }
}
