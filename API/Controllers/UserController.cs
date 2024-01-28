using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Request.AuthenticationRequest;
using Services.Request.UserRequest;
using Services.Response;
using Services.Services.UserService;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;
        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            return Ok(await _userService.GetAllUsersAsync());
        }

        [HttpGet("GetUserById/{id}")]
        public async Task<ActionResult> GetUserById(string id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(new BaseResponse(true,"",user));
        }
        [HttpGet("GetUserByIdForUserEdit/{id}")]
        public async Task<ActionResult> GetUserByIdForUserEdit(string id)
        {
            var user = await _userService.GetUserByIdForUserEdit(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(new BaseResponse(true, "", user));
        }
        [HttpGet("sendVerifyCode/{phoneNumber}")]
        public async Task<ActionResult> SendVerifyCode(string phoneNumber)
        {
            var verifyCode = await _userService.SendVerifyCode(phoneNumber);
            return Ok(new BaseResponse(true, "", verifyCode));
        }
        [HttpPost("verifyPhone")]
        public async Task<IActionResult> VerifyPhone([FromBody] VerifyRequest verifyRequest)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.VerifyPhone(verifyRequest);
                return Ok(result);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        [HttpPost("updateUser")]
        public async Task<IActionResult> UpdateUser([FromBody] UserUpdateRequest request)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.UpdateUserAsync(request);
                return Ok(result);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}
