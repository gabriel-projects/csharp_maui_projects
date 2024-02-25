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
    public class SchedulesController : ControllerBase
    {
        private readonly IScheduleRepository ScheduleRepository;

        public SchedulesController(IScheduleRepository scheduleRepository)
        {
            this.ScheduleRepository = scheduleRepository;
        }

        /// <summary>
        /// Marcar agendamento
        /// </summary>
        /// <returns></returns>
        [HttpPost("schedule/uid/{scheduleUid}/appointment")]
        public async Task<ActionResult<WrapperOutSchedule>> ScheduleAppointmentMark(Guid scheduleUid)
        {
            //todo: autorização
            var schedule = await ScheduleRepository.Get(scheduleUid);
            if (schedule == null) return new BadRequestObjectResult(new WrapperError() { Message = "Agendamento não encontrado" });  

            //regras

            var wp = await WrapperOutSchedule.From(schedule).ConfigureAwait(false);
            return Ok(wp);
        }

        /// <summary>
        /// Agendamentos disponiveis
        /// </summary>
        /// <returns></returns>
        [HttpGet("schedules/availables")]
        public async Task<ActionResult<List<WrapperOutSchedule>>> SchedulesAvailable()
        {
            //todo: obter os compromissos pelo jwt autorização
            
            var schedules = await ScheduleRepository.Schedules();

            var wp = await WrapperOutSchedule.From(schedules).ConfigureAwait(false);
            return Ok(wp);
        }

        /// <summary>
        /// Agendamentos marcados
        /// </summary>
        /// <returns></returns>
        [HttpGet("scheduled/appointments")]
        public async Task<ActionResult<List<WrapperOutSchedule>>> ScheduledAppointments()
        {
            //todo: obter os compromissos pelo jwt autorização

            var schedules = await ScheduleRepository.Schedules();

            var wp = await WrapperOutSchedule.From(schedules).ConfigureAwait(false);
            return Ok(wp);
        }
    }
}
