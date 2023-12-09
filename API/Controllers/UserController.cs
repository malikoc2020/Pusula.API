using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Request.AuthenticationRequest;
using Services.Response;
using Services.UserService;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    //[Authorize]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;
        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            return Ok(await _userService.GetAllUsersAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetUserById(string id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(new BaseResponse(true,"",user));
        }

        //[HttpPost]
        //public async Task<ActionResult> CreateUser(User user)
        //{
        //    await _userService.CreateUserAsync(user);
        //    return CreatedAtAction("GetUserById", new { id = user.Id }, user);
        //}
        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateUser(string id, User user)
        //{
        //    if (id != user.Id)
        //    {
        //        return BadRequest();
        //    }
        //    await _userService.UpdateUserAsync(user);
        //    return NoContent();
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteUser(int id)
        //{
        //    await _userService.DeleteUserAsync(id);
        //    return NoContent();
        //}
    }
}
