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
using Microsoft.EntityFrameworkCore;

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
            return Ok(res.Map<PlanListDTO<ProductionRegistration>, PlanListDTO<PreProductionItemDTO>>());
        }

        [HttpGet("{id}")]
        [Authorize]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var res = _repository.GetById(id);
            if (res == null) return NotFound();
            return Ok(res.Map<ProductionRegistration, ProductionRegistrationDTO>());
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create([FromBody] ProductionRegistrationDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var userIdClaim = User.FindFirst(ClaimName.Sub)?.Value;
            if (userIdClaim == null) return Unauthorized("User ID not found in claims.");

            var user = _userManager.Users
                .Include(u => u.SiteMap)
                .FirstOrDefaultAsync(u => u.Id == userIdClaim)
                .GetAwaiter().GetResult();
            if (user == null) return NotFound("User not found.");

            var data = dto.Map<ProductionRegistrationDTO, ProductionRegistration>();
            data.Creator = user;
            data.SiteMap = user.SiteMap;
            data.CreatedDate = DateTime.Now;
            data.Content = string.Empty;
            data.IsPersonal = true;
            data.IsDeleted = false;
            data.Status = PlanStatus.InProgress;

            var result = _repository.Create(data);
            if (result == null) return StatusCode(500, "Failed to create");

            return Ok(result.Map<ProductionRegistration, ProductionRegistrationDTO>());
        }

        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Update([FromRoute] Guid id, [FromBody] ProductionRegistrationDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var data = _repository.GetById(id);
            if (data == null) return NotFound();
            data.Title = dto.Title;
            data.Content = dto.Content;
            data.Airdate = dto.Airdate;
            data.IsPersonal = dto.IsPersonal;
            var result = _repository.Update(id, data);
            if (result == null) return StatusCode(500, "Failed to update");

            return Ok(result.Map<ProductionRegistration, ProductionRegistrationDTO>());
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
            return Ok(result.Map<ProductionRegistration, ProductionRegistrationDTO>());
        }

        [HttpGet("Status")]
        [Authorize]
        public IActionResult GetByStatus([FromBody] string status)
        {
            var res = _repository.GetByStatus(status);
            return Ok(res.Select(i => i.Map<ProductionRegistration, ProductionRegistrationDTO>()));
        }
    }
}
