using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using TVStation.Data.Constant;
using TVStation.Data.Model;
using TVStation.Data.Model.Plans;
using TVStation.Data.Model.Plans.Productions;
using TVStation.Data.QueryObject.Plans.Productions;
using TVStation.Data.Request;
using TVStation.Data.Response;
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
            var res = _repository.GetAll(query) as PlanListRes<MediaProject>;
            return Ok(new PlanListRes<MediaProjectListItemRes>
            {
                Data = res.Data.Select(m => new MediaProjectListItemRes(m)),
                ApprovedCount = res.ApprovedCount,
                InProgressCount = res.InProgressCount,
                TotalCount = res.TotalCount,
                WaitingApprovalCount = res.WaitingApprovalCount
            });
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
                Sector = req.Sector,
                Title = req.Title,
                Content = string.Empty,
                IsPersonal = true,
                MediaUrl = req.MediaUrl,
                CreatedDate = DateTime.UtcNow,
                Creator = user,
                SiteMap = user.SiteMap,
                Status = PlanStatus.WaitingForApproval,
                IsDeleted = false,
            };


            var result = _repository.Create(mediaProject);
            if (result == null) return StatusCode(500, "Failed to create MediaProject.");

            return StatusCode(200, "Created MediaProject successfully");
        }
        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Update([FromRoute] Guid id,[FromBody] MediaProjectUpdateReq req)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var data = _repository.GetById(id);
            if (data == null) return NotFound("User not found.");
            data.Title = req.Title;
            data.Content = req.Content;
            data.IsPersonal = req.IsPersonal;
            var result = _repository.Update(id, data);
            if (result == null) return StatusCode(500, "Failed to update MediaProject.");

            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete([FromRoute] Guid id)
        {
            var result = _repository.Delete(id);
            if (result == null) return StatusCode(500, "Failed to delete MediaProject.");
            return NoContent();
        }
    }
}
