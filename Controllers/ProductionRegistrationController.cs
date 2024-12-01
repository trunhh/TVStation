using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TVStation.Data.Constant;
using TVStation.Data.Model.Plans;
using TVStation.Data.Model;
using TVStation.Data.QueryObject.Plans.Productions;
using TVStation.Data.DTO.Plans;
using TVStation.Repositories.IRepositories;
using TVStation.Data.Model.Plans.Productions;

namespace TVStation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductionRegistrationController : ControllerBase
    {
        private readonly IProductionRegistrationRepository _repository;
        private readonly UserManager<User> _userManager;
        public ProductionRegistrationController(IProductionRegistrationRepository repository, UserManager<User> userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetAllPaging([FromQuery] ProductionRegistrationQuery query)
        {
            var res = _repository.GetAllPaging(query);
            return Ok(res.Map<PlanListDTO<ProductionRegistration>, PlanListDTO<MediaProjectItemDTO>>());
        }


        [HttpGet("{id}")]
        [Authorize]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var res = _repository.GetById(id);
            if (res == null) return NotFound();
            return Ok(new
            {
                Select = res
            });
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

            var mediaProject = new MediaProject
            {
                Sector = dto.Sector,
                Title = dto.Title,
                Content = string.Empty,
                IsPersonal = true,
                MediaUrl = dto.MediaUrl,
                CreatedDate = DateTime.UtcNow,
                Creator = user,
                SiteMap = user.SiteMap,
                Status = PlanStatus.WaitingForApproval,
                IsDeleted = false,
            };


            var result = _repository.Create(mediaProject);
            if (result == null) return StatusCode(500, "Failed to create");

            return Ok(result);
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

            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete([FromRoute] Guid id)
        {
            var result = _repository.Delete(id);
            if (result == null) return StatusCode(500, "Failed to delete");
            return NoContent();
        }
    }
}
