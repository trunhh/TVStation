using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TVStation.Data.Model;
using TVStation.Data.DTO;
using TVStation.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using TVStation.Data.Constant;

namespace TVStation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<User> _signInManager;
        
        public AuthController(UserManager<User> userManager, ITokenService tokenService, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
            
        }

        [HttpPost("SignIn")]
        public IActionResult Login(LoginDTO req)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var user = _userManager.Users.Include(u => u.SiteMap).FirstOrDefault(u => u.UserName == req.Username);
            if (user == null) return Unauthorized("Wrong username or password!");
            var result = _signInManager.CheckPasswordSignInAsync(user, req.Password, false).GetAwaiter().GetResult();
            if (!result.Succeeded) return Unauthorized("Wrong username or password!");
            var dto = user.Map<User, AuthDTO>();

            dto.Token = _tokenService.CreateToken(user);
            dto.SiteMapName = user.SiteMap?.Name ?? "Chưa phân bổ";
            var roles = _userManager.GetRolesAsync(user).GetAwaiter().GetResult();
            if (roles != null && roles.Count > 0) dto.Role = string.Join(',',roles);
            return Ok(dto);
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
