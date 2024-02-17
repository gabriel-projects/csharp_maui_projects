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
    public class ScheduleController : ControllerBase
    {
        private readonly IScheduleRepository ScheduleRepository;

        public ScheduleController(IScheduleRepository scheduleRepository)
        {
            ScheduleRepository = scheduleRepository;
        }

        [HttpPost("schedule")]
        public async Task<ActionResult<WrapperOutSchedule>> CreateSchedule([FromBody] WrapperInSchedule<ISchedule> wrapperInSchedule)
        {
            //todo: autorização
            var schedule = await wrapperInSchedule.Result();

            schedule = await ScheduleRepository.Insert(schedule);

            //todo: obter melhor mensagem para tratar o erro
            if (schedule == null) return new NotFoundObjectResult(new WrapperError { Message = "Falha ao criar apontamento, tente novamente." });

            var wp = await WrapperOutSchedule.From(new Schedule()).ConfigureAwait(false);
            return Ok(wp);
        }

        [HttpGet("schedules")]
        public async Task<ActionResult<List<WrapperOutSchedule>>> Schedules()
        {
            //todo: obter os compromissos pelo jwt autorização

            var schedule = await ScheduleRepository.Schedules();

            var wp = await WrapperOutSchedule.From(schedule).ConfigureAwait(false);
            return Ok(wp);
        }
    }
}
