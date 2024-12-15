using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TVStation.Data.Constant;
using TVStation.Data.DTO;
using TVStation.Data.DTO.Plans;
using TVStation.Data.Model;
using TVStation.Data.Model.Plans;
using TVStation.Data.Model.Plans.Productions;
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
            try
            {
                var res = _repository.GetAllPaging(query);
                return Ok(res.Map<PlanListDTO<MediaProject>, PlanListDTO<PlanDTO>>());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpGet("{id}")]
        [Authorize]
        public IActionResult GetById([FromRoute] Guid id)
        {
            try
            {
                var res = _repository.GetById(id);
                if (res == null) return NotFound();
                return Ok(res.Map<MediaProject, MediaProjectDTO>());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpPost]
        [Authorize]
        public IActionResult Create([FromBody] MediaProjectCreateDTO dto)
        {
            try
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
                data.Creator = user;
                data.SiteMap = user.SiteMap;
                data.CreatedDate = DateTime.Now;
                data.IsDeleted = false;
                data.Status = PlanStatus.InProgress;


                var result = _repository.Create(data);
                if (result == null) return StatusCode(500, "Failed to create");

                return Ok(result.Map<MediaProject, MediaProjectDTO>());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Update([FromRoute] Guid id, [FromBody] MediaProjectDTO dto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var result = _repository.Update(id, dto);
                if (result == null) return StatusCode(500, "Failed to update");
                return Ok(result.Map<MediaProject, MediaProjectDTO>());
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete([FromRoute] Guid id)
        {
            try
            {
                var result = _repository.Delete(id);
                if (result == null) return StatusCode(500, "Failed to delete");
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("Status")]
        [Authorize]
        public IActionResult GetByStatus([FromBody] string status)
        {
            try
            {
                var res = _repository.GetByStatus(status);
                return Ok(res.Select(i => i.Map<MediaProject, MediaProjectDTO>()));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
