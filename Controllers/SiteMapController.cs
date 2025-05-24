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
            return Ok(_repository.GetAll().Select(sm => new SimpleDTO { Value=sm.Id.ToString(), Label=sm.Name }));
        }


        [HttpGet("{id}")]
        [Authorize]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var res = _repository.GetById(id);
            if (res == null) return NotFound();
            return Ok(new SimpleDTO { Value = res.Id.ToString(), Label = res.Name });
        }

        [HttpPost]
       // [Authorize(Roles = UserRole.Admin)]
        public IActionResult Create([FromBody] SimpleReqDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var siteMap = new SiteMap
            {
                Name = dto.Value,
                CreatedDate = DateTime.Now,
            };

            var result = _repository.Create(siteMap);
            if (result == null) return StatusCode(500);

            return Ok(new SimpleDTO { Value = result.Id.ToString(), Label = result.Name });
        }

        [HttpPut("{id}")]
        [Authorize(Roles = UserRole.Admin)]
        public IActionResult Update([FromRoute] Guid id, [FromBody] SimpleReqDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var data = _repository.GetById(id);
            if (data == null) return NotFound();
            data.Name = dto.Value;
            var result = _repository.Update(id, data);
            if (result == null) return StatusCode(500);

            return Ok(new SimpleDTO { Value = result.Id.ToString(), Label = result.Name });
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
