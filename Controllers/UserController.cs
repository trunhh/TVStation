using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TVStation.Data.Constant;
using TVStation.Data.Model;
using TVStation.Data.QueryObject;
using TVStation.Data.Request;
using TVStation.Data.Response;
using TVStation.Repositories.IRepositories;

namespace TVStation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        public UserController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        [HttpGet("{username}")]
        [Authorize]
        public IActionResult GetByUsername([FromRoute] string username)
        {
            var user = _userManager.Users.FirstOrDefault(u => u.UserName == username);
            if (user == null) return Unauthorized("Invalid username!");
            return Ok(new UserRes
            {
                UserName = user.UserName,
                Email = user.Email,
                Name = user.Name,
            });
        }

        [HttpGet]
        [Authorize]
        public IActionResult Get([FromQuery]UserQuery query)
        {
            var queryable = _userManager.Users;
            if (query.SiteMapId != null && query.SiteMapId != Guid.Empty)
            {
                queryable = queryable.Include(u => u.SiteMap)
                    .AsQueryable()
                    .Where(u => u.SiteMap != null && u.SiteMap.Id == query.SiteMapId);
            }
            if (!string.IsNullOrEmpty(query.Keyword))
            {
                queryable = queryable.Where(u => u.Name.Contains(query.Keyword)
                                                || (u.UserName != null && u.UserName.Contains(query.Keyword))
                                                || (u.Email != null && u.Email.Contains(query.Keyword)));
            }

            return Ok(new ListRes<User>
            {
                Data = queryable.Skip((query.PageIndex - 1) * Config.PageSize).Take(Config.PageSize).ToList(),
            });
        }

        [HttpPut]
        [Authorize]
        public IActionResult Update([FromBody]UserUpdateReq req)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var userIdClaim = User.FindFirst(ClaimName.Sub)?.Value;
            if (userIdClaim == null) return Unauthorized("User ID not found in claims.");
            var user = _userManager.Users
                .Include(u => u.SiteMap)
                .FirstOrDefaultAsync(u => u.Id == userIdClaim)
                .GetAwaiter().GetResult();
            if (user == null) return NotFound("User not found.");
            if (!string.IsNullOrEmpty(req.Address)) user.Address = req.Address;
            if (!string.IsNullOrEmpty(req.Email)) user.Email = req.Email;
            if (!string.IsNullOrEmpty(req.Name)) user.Name = req.Name;
            if (!string.IsNullOrEmpty(req.PhoneNumber)) user.PhoneNumber = req.PhoneNumber;
            var update = _userManager.UpdateAsync(user).GetAwaiter().GetResult();
            if (!update.Succeeded) return StatusCode(500, "Failed to update user info.");
            return Ok(user);
        }
    }
}
