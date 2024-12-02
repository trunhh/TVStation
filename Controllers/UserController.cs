using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TVStation.Data.Constant;
using TVStation.Data.Model;
using TVStation.Data.QueryObject;
using TVStation.Data.DTO;
using TVStation.Services;

namespace TVStation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IFileUploadService _uploadService;
        public UserController(UserManager<User> userManager, IFileUploadService uploadService)
        {
            _userManager = userManager;
            _uploadService = uploadService;
        }
        [HttpGet("{username}")]
        [Authorize]
        public IActionResult GetByUsername([FromRoute] string username)
        {
            var user = _userManager.Users.FirstOrDefault(u => u.UserName == username);
            if (user == null) return Unauthorized("Invalid username!");
            return Ok(user.Map<User, UserDTO>());
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetAll([FromQuery]UserQuery query)
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

            return Ok(queryable.Select(u => u.Map<User, UserDTO>()));
        }

        [HttpPut]
        [Authorize]
        public IActionResult Update([FromBody]UserUpdateDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var userIdClaim = User.FindFirst(ClaimName.Sub)?.Value;
            if (userIdClaim == null) return Unauthorized("User ID not found in claims.");
            var user = _userManager.Users
                .Include(u => u.SiteMap)
                .FirstOrDefaultAsync(u => u.Id == userIdClaim)
                .GetAwaiter().GetResult();
            if (user == null) return NotFound("User not found.");
            if (!string.IsNullOrEmpty(dto.Address)) user.Address = dto.Address;
            if (!string.IsNullOrEmpty(dto.Email)) user.Email = dto.Email;
            if (!string.IsNullOrEmpty(dto.Name)) user.Name = dto.Name;
            if (!string.IsNullOrEmpty(dto.PhoneNumber)) user.PhoneNumber = dto.PhoneNumber;
            if (dto.Avatar != null &&  dto.Avatar.Length > 0) user.AvatarUrl = _uploadService.UploadFile(dto.Avatar);
            var update = _userManager.UpdateAsync(user).GetAwaiter().GetResult();
            if (!update.Succeeded) return StatusCode(500, "Failed to update user info.");
            return Ok(user.Map<User, UserDTO>());
        }
    }
}
