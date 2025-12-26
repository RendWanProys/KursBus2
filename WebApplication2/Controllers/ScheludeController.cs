using KursBus2.Models;
using KursBus2.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace KursBus2.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class ScheduleController : ControllerBase
    {
        private readonly ScheduleService ScheduleService;

        public ScheduleController(ScheduleService service)
        {
            this.ScheduleService = service;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Schedule>>> GetAllSchedules()
        {
            return Ok(await ScheduleService.GetAll());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Schedule>> GetScheduleById(int id)
        {
            var chit = await ScheduleService.GetById(id);
            if (chit == null) return NotFound();
            return Ok(chit);
        }
        [HttpPost]
        public async Task<ActionResult<Schedule>> CreateSchedule([FromBody] Schedule chit)
        {
            await ScheduleService.Create(chit);
            return CreatedAtAction(nameof(GetScheduleById), new { Id = chit.TripId }, chit);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Schedule>> UpdateSchedule(int id, [FromBody] Schedule chit)
        {
            if (chit.TripId != id) return BadRequest();
            await ScheduleService.Update(chit);
            return Ok(chit);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await ScheduleService.Delete(id);
            return NoContent();
        }
    }
}
