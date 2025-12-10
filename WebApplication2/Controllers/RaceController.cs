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
    public class RaceController : ControllerBase
    {
        private readonly RaceService RaceService;

        public RaceController(RaceService service)
        {
            this.RaceService = service;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Race>>> GetAllChitatels()
        {
            return Ok(await RaceService.GetAll());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Race>> GetChitatelById(int id)
        {
            var chit = await RaceService.GetById(id);
            if (chit == null) return NotFound();
            return Ok(chit);
        }
        [HttpPost]
        public async Task<ActionResult<Race>> CreateChitatel([FromBody] Race chit)
        {
            await RaceService.Create(chit);
            return CreatedAtAction(nameof(GetChitatelById), new { Id = chit.RaceId }, chit);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Race>> UpdateChitatel(int id, [FromBody] Race chit)
        {
            if (chit.RaceId != id) return BadRequest();
            await RaceService.Update(chit);
            return Ok(chit);
        }
        [HttpDelete("id")]
        public async Task<IActionResult> Delete(int id)
        {
            await RaceService.Delete(id);
            return NoContent();
        }
    }
}
