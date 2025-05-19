using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TVStation.Data.Constant;
using TVStation.Data.DTO.Plans;
using TVStation.Data.Model;
using TVStation.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using TVStation.Data.QueryObject;

namespace TVStation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : ControllerBase
    {
        private readonly IEventRepository _repository;
        private readonly IChannelRepository _channelRepository;
        private readonly UserManager<User> _userManager;
        public EventController(IEventRepository repository, IChannelRepository channelRepository, UserManager<User> userManager)
        {
            _repository = repository;
            _channelRepository = channelRepository;
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetAllPaging([FromQuery] EventQuery query)
        {
            try
            {
                var res = _repository.GetAll(query);
                return Ok(res);
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
                return Ok(res.Map<Event, EventReqDTO>());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpPost]
        [Authorize]
        public IActionResult Create([FromBody] EventReqDTO dto)
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

                var data = dto.Map<EventReqDTO, Event>();
                data.Channel = _channelRepository.GetById(dto.ChannelId);
                data.Collaborators = dto.CollaboratorUsername
                                    .Select(username => _userManager.Users.FirstOrDefault(u => u.UserName == username))
                                    .Where(user => user != null)
                                    .Select(user => user!) // null-forgiving operator
                                    .ToList();
                data.Collaborators.Add(user);
                data.Collaborators.Distinct();

                data.Creator = user;
                data.CreatedDate = DateTime.Now;
                data.IsDeleted = false;
                data.Status = PlanStatus.InProgress;

                var result = _repository.Create(data);
                if (result == null) return StatusCode(500, "Failed to create");

                return Ok(result.Map<Event, EventResDTO>());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Update([FromRoute] Guid id, [FromBody] object dto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var result = _repository.Update(id, dto);
                if (result == null) return StatusCode(500, "Failed to update");
                return Ok(result.Map<Event, EventReqDTO>());
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
