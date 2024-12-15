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


        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var userIdClaim = User.FindFirst(ClaimName.Sub)?.Value;
            if (userIdClaim == null) return Unauthorized("User ID not found in claims.");
            var user = _userManager.Users
                .Include(u => u.SiteMap)
                .FirstOrDefaultAsync(u => u.Id == userIdClaim)
                .GetAwaiter().GetResult();
            if (user == null) return NotFound("User not found.");
            return Ok(user.Map<User, UserDTO>());
        }

        [HttpPut]
        [Authorize]
        public IActionResult Update([FromBody]UserDTO dto)
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
            user.Address = dto.Address;
            var update = _userManager.UpdateAsync(user).GetAwaiter().GetResult();
            if (!update.Succeeded) return StatusCode(500, "Failed to update user info.");
            return Ok(user.Map<User, UserDTO>());
        }

        [HttpPut("Password")]
        [Authorize]
        public IActionResult UpdatePassword([FromBody] UpdatePasswordDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var userIdClaim = User.FindFirst(ClaimName.Sub)?.Value;
            if (userIdClaim == null) return Unauthorized("User ID not found in claims.");
            var user = _userManager.Users
                .Include(u => u.SiteMap)
                .FirstOrDefaultAsync(u => u.Id == userIdClaim)
                .GetAwaiter().GetResult();
            if (user == null) return NotFound("User not found.");
            var update = _userManager.ChangePasswordAsync(user, dto.OldPassword, dto.NewPassword).GetAwaiter().GetResult();
            if (!update.Succeeded) return Unauthorized(update.Errors.First().Description);
            return Ok();
        }
    }
}
