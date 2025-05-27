using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TVStation.Data.Constant;
using TVStation.Data.Model;
using TVStation.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using TVStation.Data.QueryObject;
using TVStation.Data.DTO.Event;
using TVStation.Data.DTO;
using TVStation.Services;

namespace TVStation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EpisodeController : ControllerBase
    {
        private readonly IEpisodeRepository _repository;
        private readonly IChannelRepository _channelRepository;
        private readonly IProgrammeRepository _programmeRepository;
        private readonly ISiteMapRepository _siteMapRepository;
        private readonly UserManager<User> _userManager;
        public EpisodeController(IEpisodeRepository repository, IChannelRepository channelRepository,
            IProgrammeRepository programmeRepository, ISiteMapRepository siteMapRepository, UserManager<User> userManager)
        {
            _repository = repository;
            _channelRepository = channelRepository;
            _programmeRepository = programmeRepository;
            _siteMapRepository = siteMapRepository;
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetAllPaging([FromQuery] EpisodeQuery query)
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
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpPost()]
        [Authorize]
        public IActionResult Create([FromBody] SimpleDTO dto)
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

                var programme = _programmeRepository.GetById(Guid.Parse(dto.Value));
                if (programme == null) return NotFound("Program not found");

                var lastEp = programme.Episodes.OrderByDescending(e => e.Start.Date).FirstOrDefault();

                var occurs = OccurencesUtils.GetOccurrences(lastEp.Start, programme.Frequency, 2).Last();

                var ep = new Episode
                {
                    Start = occurs,
                    End = occurs.AddHours(programme.Duration),
                    CreatedDate = DateTime.UtcNow,
                    Status = PlanStatus.InProgress,
                    IsDeleted = false,
                    Programme = programme,
                    Index = lastEp.Index + 1,
                };

                var conflicts = _repository.CheckSchedulingConflict(ep);
                if (conflicts.Count > 0)
                {
                    var error = "Trùng lịch phát sóng: ";
                    foreach (var conflict in conflicts)
                    {
                        error += " " + conflict.Title.ToString() + ": " + conflict.Start.ToString() + "-" + conflict.End.ToString() + ";";
                    }
                    return StatusCode(500, error);
                }   

                var result = _repository.Create(ep);
                if (result == null) return StatusCode(500, "Failed to create");

                return Ok(result.Map<Episode, EpisodeConfigDTO>());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Update([FromRoute] Guid id, [FromBody] EpisodeConfigDTO dto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var result = _repository.Update(id, dto);
                if (result == null) return StatusCode(500, "Failed to update");
                return Ok(result);
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
