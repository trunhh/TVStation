using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TVStation.Data.Constant;
using TVStation.Data.DTO;
using TVStation.Data.DTO.Plans;
using TVStation.Data.Model;
using TVStation.Data.Model.Plans;
using TVStation.Data.QueryObject.Plans.Productions;
using TVStation.Repositories.IRepositories;

namespace TVStation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MediaProjectController : ControllerBase
    {
        private readonly IMediaProjectRepository _repository;
        private readonly UserManager<User> _userManager;
        public MediaProjectController(IMediaProjectRepository repository, UserManager<User> userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetAllPaging([FromQuery] MediaProjectQuery query)
        {
            var res = _repository.GetAllPaging(query);
            return Ok(res.Map<PlanListDTO<MediaProject>, PlanListDTO<MediaProjectItemDTO>>());
        }

        [HttpGet("{id}")]
        [Authorize]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var res = _repository.GetById(id);
            if (res == null) return NotFound();
            return Ok(res.Map<MediaProject,MediaProjectDTO>());
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create([FromBody] MediaProjectCreateDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var userIdClaim = User.FindFirst(ClaimName.Sub)?.Value;
            if (userIdClaim == null) return Unauthorized("User ID not found in claims.");

            var user = _userManager.Users
                .Include(u => u.SiteMap)
                .FirstOrDefaultAsync(u => u.Id == userIdClaim)
                .GetAwaiter().GetResult();
            if (user == null) return NotFound("User not found.");

            var data = dto.Map<MediaProjectCreateDTO, MediaProject>();
            data.CreatedDate = DateTime.Now;
            data.Content = string.Empty;
            data.IsPersonal = true;
            data.IsDeleted = false;
            data.Status = PlanStatus.InProgress;

            var result = _repository.Create(data);
            if (result == null) return StatusCode(500, "Failed to create");

            return Ok(result.Map<MediaProject, MediaProjectDTO>());
        }

        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Update([FromRoute] Guid id, [FromBody] MediaProjectDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var data = _repository.GetById(id);
            if (data == null) return NotFound();
            data.Title = dto.Title;
            data.Content = dto.Content;
            data.IsPersonal = dto.IsPersonal;
            var result = _repository.Update(id, data);
            if (result == null) return StatusCode(500, "Failed to update");

            return Ok(result.Map<MediaProject, MediaProjectDTO>());
        }

        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete([FromRoute] Guid id)
        {
            var result = _repository.Delete(id);
            if (result == null) return StatusCode(500, "Failed to delete");
            return NoContent();
        }

        [HttpPut("Status/{id}")]
        [Authorize]
        public IActionResult UpdateStatus([FromRoute] Guid id, [FromBody] string status)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = _repository.SetStatus(id, status);
            if (result == null) return BadRequest(status);
            return Ok(result.Map<MediaProject, MediaProjectDTO>());
        }

        [HttpGet("Status")]
        [Authorize]
        public IActionResult GetByStatus([FromBody] string status)
        {
            var res = _repository.GetByStatus(status);
            return Ok(res.Select(i => i.Map<MediaProject, MediaProjectItemDTO>()));
        }
    }
}
