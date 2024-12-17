using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TVStation.Data.Constant;
using TVStation.Data.DTO;
using TVStation.Data.Model;
using TVStation.Data.QueryObject;
using TVStation.Repositories.IRepositories;

namespace TVStation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly ISiteMapRepository _siteMapRepository;
        public UsersController(UserManager<User> userManager, ISiteMapRepository siteMapRepository)
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
        [HttpGet("{username}")]
        [Authorize(Roles = UserRole.Admin)]
        public IActionResult GetByUsername([FromRoute] string username)
        {
            var user = _userManager.Users.FirstOrDefault(u => u.UserName == username);
            if (user == null) return Unauthorized("Invalid username!");
            return Ok(user.Map<User, UserDTO>());
        }


        [HttpGet]
        [Authorize(Roles = UserRole.Admin)]
        public IActionResult GetAll([FromQuery] UserQuery query)
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
    }
}
