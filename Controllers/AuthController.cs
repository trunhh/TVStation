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

        [HttpPost("SignUp")]
        public IActionResult Register([FromBody] RegisterDTO dto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var user = new User
                {
                    UserName = dto.Username,
                    Email = dto.Email,
                    Name = dto.Name
                };

                var createUser = _userManager.CreateAsync(user, dto.Password).GetAwaiter().GetResult();

                if (createUser.Succeeded)
                {
                    var roleResult = _userManager.AddToRoleAsync(user, "Employee").GetAwaiter().GetResult();
                    if (roleResult.Succeeded)
                        return Ok(new AuthDTO
                        {
                            UserName = user.UserName,
                            Email = user.Email,
                            Token = _tokenService.CreateToken(user)
                        });
                    else return StatusCode(500, roleResult.Errors);

                }
                else return StatusCode(500, createUser.Errors);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }


        }

        [HttpPost("SignIn")]
        public IActionResult Login(LoginDTO req)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var user = _userManager.Users.FirstOrDefault(u => u.UserName == req.Username);
            if (user == null) return Unauthorized("Invalid username!");
            var result = _signInManager.CheckPasswordSignInAsync(user, req.Password, false).GetAwaiter().GetResult();
            if (!result.Succeeded) return Unauthorized("Wrong password!");
            return Ok(new AuthDTO
            {
                UserName = user.UserName,
                Email = user.Email,
                Token = _tokenService.CreateToken(user)
            });
        }
    }
}
