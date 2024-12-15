using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TVStation.Data.Model;
using TVStation.Data.DTO;
using TVStation.Services;

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
            var user = _userManager.Users.FirstOrDefault(u => u.UserName == req.Username);
            if (user == null) return Unauthorized("Wrong username or password!");
            var result = _signInManager.CheckPasswordSignInAsync(user, req.Password, false).GetAwaiter().GetResult();
            if (!result.Succeeded) return Unauthorized("Wrong username or password!");
            var dto = new AuthDTO
            {
                UserName = user.UserName,
                AvatarUrl = user.AvatarUrl,
                Name = user.Name,
                Email = user.Email,
                Token = _tokenService.CreateToken(user)
            };
            var roles = _userManager.GetRolesAsync(user).GetAwaiter().GetResult();
            if (roles != null && roles.Count > 0) dto.Role = roles[0];
            return Ok(dto);
        }
    }
}
