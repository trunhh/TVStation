using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TVStation.Data.Constant;
using TVStation.Data.DTO.Plans;
using TVStation.Data.Model.Plans.Productions;
using TVStation.Data.Model;
using TVStation.Data.QueryObject.Plans.Productions;
using TVStation.Repositories.IRepositories;
using TVStation.Data.QueryObject.Plans.ProgramFrames;
using TVStation.Data.Model.Plans.ProgramFrames;
using Microsoft.EntityFrameworkCore;

namespace TVStation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProgramFrameBroadcastController : ControllerBase
    {
        private readonly IProgramFrameBroadcastRepository _repository;
        private readonly UserManager<User> _userManager;
        public ProgramFrameBroadcastController(IProgramFrameBroadcastRepository repository, UserManager<User> userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetAllPaging([FromQuery] ProgramFrameBroadcastQuery query)
        {
            try
            {
                var res = _repository.GetAllPaging(query);
                return Ok(res.Map<PlanListDTO<ProgramFrameBroadcast>, PlanListDTO<PlanDTO>>());
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
                return Ok(res.Map<ProgramFrameBroadcast, ProgramFrameBroadcastDTO>());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpPost]
        [Authorize]
        public IActionResult Create([FromBody] ProgramFrameBroadcastDTO dto)
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

                var data = dto.Map<ProgramFrameBroadcastDTO, ProgramFrameBroadcast>();
                data.Creator = user;
                data.CreatedDate = DateTime.Now;
                data.IsDeleted = false;
                data.Status = PlanStatus.InProgress;

                var result = _repository.Create(data);
                if (result == null) return StatusCode(500, "Failed to create");

                return Ok(result.Map<ProgramFrameBroadcast, ProgramFrameBroadcastDTO>());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Update([FromRoute] Guid id, [FromBody] ProgramFrameBroadcastDTO dto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var result = _repository.Update(id, dto);
                if (result == null) return StatusCode(500, "Failed to update");
                return Ok(result.Map<ProgramFrameBroadcast, ProgramFrameBroadcastDTO>());
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
    }
}
