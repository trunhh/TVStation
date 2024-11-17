﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using TVStation.Data.Model;
using TVStation.Data.Request;
using TVStation.Data.Response;
using TVStation.Services;

namespace TVStation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<User> _signInManager;
        public UserController(UserManager<User> userManager, ITokenService tokenService, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterReq req)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var user = new User
                {
                    UserName = req.Username,
                    Email = req.Email,
                    Name = req.Name
                };

                var createUser = _userManager.CreateAsync(user, req.Password).GetAwaiter().GetResult();

                if (createUser.Succeeded)
                {
                    var roleResult = _userManager.AddToRoleAsync(user, "Employee").GetAwaiter().GetResult();
                    if (roleResult.Succeeded)
                        return Ok(new NewUserRes
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

        [HttpPost("login")]
        public IActionResult Login(LoginReq req)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var user = _userManager.Users.FirstOrDefault(u => u.UserName == req.Username);
            if (user == null) return Unauthorized("Invalid username!");
            var result = _signInManager.CheckPasswordSignInAsync(user, req.Password, false).GetAwaiter().GetResult();
            if (!result.Succeeded) return Unauthorized("Wrong password!");
            return Ok(new NewUserRes
            {
                UserName = user.UserName,
                Email = user.Email,
                Token = _tokenService.CreateToken(user)
            });
        }
    }
}
