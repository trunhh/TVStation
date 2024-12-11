using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TVStation.Data.Model;
using TVStation.Data.DTO;
using TVStation.Repositories.IRepositories;
using TVStation.Data.Constant;

namespace TVStation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SiteMapController : ControllerBase
    {
        private readonly ISiteMapRepository _repository;

        public SiteMapController(ISiteMapRepository repository)
        {
            _repository = repository;
        }
        [HttpGet]
        [Authorize]
        public IActionResult GetAll()
        {
            return Ok(_repository.GetAll().Select(sm => sm.Map<SiteMap, SiteMapDTO>()));
        }


        [HttpGet("{id}")]
        [Authorize]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var res = _repository.GetById(id);
            if (res == null) return NotFound();
            return Ok(res.Map<SiteMap, SiteMapDTO>());
        }

        [HttpPost]
        [Authorize(Roles = UserRole.Admin)]
        public IActionResult Create([FromBody] SiteMapCreateDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var siteMap = new SiteMap
            {
                Name = dto.Name,
                CreatedDate = DateTime.Now,
            };

            var result = _repository.Create(siteMap);
            if (result == null) return StatusCode(500);

            return Ok(result.Map<SiteMap, SiteMapDTO>());
        }

        [HttpPut("{id}")]
        [Authorize(Roles = UserRole.Admin)]
        public IActionResult Update([FromRoute] Guid id, [FromBody] SiteMapDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var data = _repository.GetById(id);
            if (data == null) return NotFound();
            data.Name = dto.Name;
            var result = _repository.Update(id, data);
            if (result == null) return StatusCode(500);

            return Ok(result.Map<SiteMap, SiteMapDTO>());
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = UserRole.Admin)]
        public IActionResult Delete([FromRoute] Guid id)
        {
            var result = _repository.Delete(id);
            if (result == null) return StatusCode(500);
            return NoContent();
        }
    }
}
