using Api.GRRInnovations.Domain.Models;
using Api.GRRInnovations.Domain.Wrappers.In;
using Api.GRRInnovations.Domain.Wrappers.Out;
using Api.GRRInnovations.Domain.Wrappers;
using Api.GRRInnovations.Infrastructure.Repositories;
using Api.GRRInnovations.Interfaces.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Api.GRRInnovations.Interfaces.Repositories;

namespace Api.GRRInnovations.AgendaPro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentManagementController : ControllerBase
    {
        private readonly IAppointmentRepository AppointmentRepository;

        public AppointmentManagementController(IAppointmentRepository appointmentRepository)
        {
            AppointmentRepository = appointmentRepository;
        }

        [HttpPost("appointment/management")]
        public async Task<ActionResult<WrapperOutAppointment>> CreateAppointment([FromBody] WrapperInAppointment<IAppointment> wrapperInAppointment)
        {
            //todo: autorização


            var appointment = await wrapperInAppointment.Result();

            appointment = await AppointmentRepository.Insert(appointment);

            //todo: obter melhor mensagem para tratar o erro
            if (appointment == null) return new NotFoundObjectResult(new WrapperError { Message = "Falha ao criar apontamento, tente novamente." });

            var wp = await WrapperOutAppointment.From(new Appointment()).ConfigureAwait(false);
            return Ok(wp);
        }
    }
}
