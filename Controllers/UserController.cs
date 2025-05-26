using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TVStation.Data.Constant;
using TVStation.Data.Model;
using TVStation.Data.QueryObject;
using TVStation.Data.DTO;
using TVStation.Services;
using TVStation.Repositories.IRepositories;
using TVStation.Repositories.Repositories;

namespace TVStation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly ISiteMapRepository _siteMapRepository;
        public UserController(UserManager<User> userManager, ISiteMapRepository siteMapRepository)
        {
            _userManager = userManager;
            _siteMapRepository = siteMapRepository;
        }


        [HttpPost]
        //[Authorize(Roles = UserRole.Admin)]
        public IActionResult Register([FromBody] RegisterDTO dto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var siteMap = _siteMapRepository.GetById(dto.SiteMapId);

                var user = new User
                {
                    UserName = dto.Username,
                    SiteMap = siteMap,
                };

                var createUser = _userManager.CreateAsync(user, dto.Password).GetAwaiter().GetResult();

                if (createUser.Succeeded)
                {
                    var roleResult = _userManager.AddToRoleAsync(user, dto.Role).GetAwaiter().GetResult();
                    if (roleResult.Succeeded)
                        return Ok();
                    else return StatusCode(500, roleResult.Errors);
                }
                else return StatusCode(500, createUser.Errors);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{username}")]
        [Authorize]
        public IActionResult Update([FromRoute] string username, [FromBody]UserDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var userIdClaim = User.FindFirst(ClaimName.Sub)?.Value;
            if (userIdClaim == null) return Unauthorized("User ID not found in claims.");
            var user = _userManager.Users
                .Include(u => u.SiteMap)
                .FirstOrDefaultAsync(u => u.Id == userIdClaim)
                .GetAwaiter().GetResult();
            if (user == null) return NotFound("User not found.");
            user.Name = dto.Name;
            user.Email = dto.Email;
            user.PhoneNumber = dto.PhoneNumber;
            var update = _userManager.UpdateAsync(user).GetAwaiter().GetResult();
            if (!update.Succeeded) return StatusCode(500, "Failed to update user info.");
            return Ok(user.Map<User, UserDTO>());
        }

        [HttpGet("{username}")]
        [Authorize(Roles = UserRole.Admin)]
        public IActionResult GetByUsername([FromRoute] string username)
        {
            var userIdClaim = User.FindFirst(ClaimName.Sub)?.Value;
            if (userIdClaim == null) return Unauthorized("User ID not found in claims.");


            var user = _userManager.Users.Include(u => u.SiteMap)
                .FirstOrDefault(u => u.UserName == username);
            if (user == null) return Unauthorized("Invalid username!");
            return Ok(user.Map<User, UserDTO>());
        }

        [HttpGet("All")]
        [Authorize]
        public IActionResult GetAll() => Ok(_userManager.Users.Select(u => new SimpleDTO { Value = u.UserName, Label = u.Name }));

        [HttpGet]
        //[Authorize(Roles = UserRole.Admin)]
        public IActionResult GetAll([FromQuery] UserQuery? query)
        {
            var queryable = _userManager.Users;
            if (query == null) return Ok(queryable.Select(u => new SimpleDTO { Value = u.UserName, Label = u.Name }));
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

            return Ok(queryable.Select(u => u.Map<User, UserDTO>()));
        }
    }
}
