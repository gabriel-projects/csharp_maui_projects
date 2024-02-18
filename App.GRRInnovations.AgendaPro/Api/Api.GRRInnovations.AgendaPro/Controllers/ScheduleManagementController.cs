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
    public class ScheduleManagementController : ControllerBase
    {
        private readonly IScheduleRepository ScheduleRepository;

        public ScheduleManagementController(IScheduleRepository scheduleRepository)
        {
            ScheduleRepository = scheduleRepository;
        }

        [HttpPost("schedule/management")]
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
    }
}
