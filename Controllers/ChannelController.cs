using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TVStation.Data.Constant;
using TVStation.Data.DTO;
using TVStation.Data.Model;
using TVStation.Repositories.IRepositories;

namespace TVStation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChannelController : ControllerBase
    {
        private readonly IChannelRepository _repository;

        public ChannelController(IChannelRepository repository)
        {
            _repository = repository;
        }
        [HttpGet]
        [Authorize]
        public IActionResult GetAll()
        {
            return Ok(_repository.GetAll());
        }


        [HttpGet("{id}")]
        [Authorize]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var res = _repository.GetById(id);
            if (res == null) return NotFound();
            return Ok(res);
        }

        [HttpPost]
        // [Authorize(Roles = UserRole.Admin)]
        public IActionResult Create([FromBody] ChannelCreateDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var channel = new Channel
            {
                Name = dto.Name,
                Color = dto.Color,
                BackgroundColor = dto.BackgroundColor,
                BorderColor = dto.BorderColor,
                DragBackgroundColor = dto.DragBackgroundColor,
            };

            var result = _repository.Create(channel);
            if (result == null) return StatusCode(500);

            return Ok(result);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = UserRole.Admin)]
        public IActionResult Update([FromRoute] Guid id, [FromBody] Channel dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var data = _repository.GetById(id);
            if (data == null) return NotFound();
            data.Name = dto.Name;
            var result = _repository.Update(id, data);
            if (result == null) return StatusCode(500);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = UserRole.Admin)]
        public IActionResult Delete([FromRoute] Guid id)
        {
            var result = _repository.Delete(id);
            if (result == null) return StatusCode(500);
            return NoContent();
        }
    }
}
