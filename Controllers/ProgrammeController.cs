using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TVStation.Data.Constant;
using TVStation.Data.DTO.Plans;
using TVStation.Data.Model;
using TVStation.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using TVStation.Data.QueryObject;
using TVStation.Data.DTO.Event;
using TVStation.Services;

namespace TVStation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProgrammeController : ControllerBase
    {
        private readonly IProgrammeRepository _repository;
        private readonly IChannelRepository _channelRepository;
        private readonly IEpisodeRepository _episodeRepository;
        private readonly ISiteMapRepository _siteMapRepository;
        private readonly UserManager<User> _userManager;
        public ProgrammeController(IProgrammeRepository repository, IChannelRepository channelRepository, 
            IEpisodeRepository episodeRepository, ISiteMapRepository siteMapRepository, UserManager<User> userManager)
        {
            _repository = repository;
            _channelRepository = channelRepository;
            _episodeRepository = episodeRepository;
            _siteMapRepository = siteMapRepository;
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetAllPaging([FromQuery] ProgrammeQuery query)
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

        [HttpPost]
        [Authorize]
        public IActionResult Create([FromBody] ProgrammeConfigDTO dto)
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

                var prog = dto.Map<ProgrammeConfigDTO, Programme>();
                prog.Channel = _channelRepository.GetById(dto.ChannelId);
                prog.Collaborators = new List<Collab> { new Collab { User = user, Programme = prog } };
                prog.SiteMap = _siteMapRepository.GetById(dto.SiteMapId);
                if (prog.SiteMap != null)
                {
                    foreach (var member in prog.SiteMap.Members)
                    {
                        prog.Collaborators.Add(new Collab { User = member, Programme = prog });
                    }
                }
                prog.Owner = user;
                prog.CreatedDate = DateTime.Now;
                prog.IsDeleted = false;
                prog.Status = PlanStatus.InProgress;
                prog.Episodes = new List<Episode>();
                
                
                var occurs = OccurencesUtils.GetOccurrences(dto.StartDate.ToDateTime(dto.StartTime), dto.Frequency, dto.EpisodeNumber).ToList();

                for (int i=0; i < occurs.Count(); i++)
                {
                    var episode = new Episode
                    {
                        Programme = prog,
                        Index = i + 1,
                        Status = PlanStatus.InProgress,
                        Start = occurs[i],
                        End = occurs[i].AddHours(dto.Duration)
                    };
                    var conflicts = _episodeRepository.CheckSchedulingConflict(episode);
                    if (conflicts.Count > 0)
                    {
                        var error = "Trùng lịch phát sóng: ";
                        foreach (var conflict in conflicts) 
                        { 
                            error += " " + conflict.Title.ToString() + ": " + conflict.Start.ToString() + "-" + conflict.End.ToString() + ";"; 
                        }
                        return StatusCode(500, error);
                    }
                    prog.Episodes.Add(episode);
                }

                var result = _repository.Create(prog);
                if (result == null) return StatusCode(500, "Failed to create");

                return Ok(result.Map<Programme, ProgrammeConfigDTO>());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Update([FromRoute] Guid id, [FromBody] ProgrammeConfigDTO dto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                dto.Channel = _channelRepository.GetById(dto.ChannelId);
                dto.SiteMap = _siteMapRepository.GetById(dto.SiteMapId);
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
