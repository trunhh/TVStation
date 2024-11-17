using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using TVStation.Data.Constant;
using TVStation.Data.Model;
using TVStation.Data.Model.Plans.Productions;
using TVStation.Data.QueryObject.Plans.Productions;
using TVStation.Data.Request;
using TVStation.Repositories.IRepositories;
using TVStation.Repositories.Repositories.PlanRepositories.ProductionPlanRepositories;

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
        public IActionResult GetAll([FromQuery] MediaProjectQuery query)
        {
            var data =_repository.GetAll(query);
            return Ok(data);
        }
        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute]Guid id)
        {
            var res = _repository.GetById(id);
            if (res == null) return NotFound();
            return Ok(res);
        }
        [HttpPost]
        [Authorize]
        public IActionResult Create([FromBody] MediaProjectCreateReq req) 
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var userIdClaim = User.FindFirst(ClaimName.Sub)?.Value; 
            if (userIdClaim == null) return Unauthorized("User ID not found in claims.");

            var user = _userManager.Users
                .Include(u => u.SiteMap)
                .FirstOrDefaultAsync(u => u.Id == userIdClaim)
                .GetAwaiter().GetResult();
            if (user == null) return NotFound("User not found.");

            var mediaProject = new MediaProject
            {
                Id = Guid.NewGuid(),
                Sector = req.Sector,
                Title = req.Title,
                Content = req.Content,
                IsPersonal = req.IsPersonal,
                Airdate = req.Airdate,
                MediaUrl = req.MediaUrl,
                CreatedDate = DateTime.UtcNow,
                Creator = user,
                SiteMap = user.SiteMap,
                Status = PlanStatus.WaitingForApproval,
                IsDeleted = false,
            };


            var result = _repository.Create(mediaProject);
            if (result == null) return StatusCode(500, "Failed to create MediaProject.");

            return Ok("MediaProject created successfully.");
        }

    }
}
