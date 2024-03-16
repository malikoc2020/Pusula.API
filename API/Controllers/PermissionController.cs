using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.DTO;
using Services.Services.PermissionService;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class PermissionController : ControllerBase
    {
        private readonly ILogger<PermissionController> _logger;
        private readonly IPermissionService _permissionService;
        public PermissionController(ILogger<PermissionController> logger, IPermissionService permissionService)
        {
            _logger = logger;
            _permissionService = permissionService;
        }

        [HttpGet("GetAllPermissions")]
        public async Task<IActionResult> GetAllPermissions()
        {
            return Ok(await _permissionService.GetAllPermissionsAsync());
        }

        [HttpGet("GetPermissionById/{id}")]
        public async Task<ActionResult> GetPermissionById(int id)
        {
            return Ok(await _permissionService.GetPermissionByIdAsync(id));
        }
        [HttpPost("InsertPermission")]
        public async Task<IActionResult> InsertPermission([FromBody] PermissionDTO request)
        {
            if (ModelState.IsValid)
            {
                var result = await _permissionService.CreatePermissionAsync(request);
                return Ok(result);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        [HttpPost("UpdatePermission")]
        public async Task<IActionResult> UpdatePermission([FromBody] PermissionDTO request)
        {
            if (ModelState.IsValid)
            {
                var result = await _permissionService.UpdatePermissionAsync(request);
                return Ok(result);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        [HttpGet("GetAllPermissionTypes")]
        public async Task<IActionResult> GetAllPermissionTypes()
        {
            return Ok(await _permissionService.GetAllPermissionTypesAsync());
        }
        [HttpDelete("DeletePermission/{id}")]
        public async Task<ActionResult> DeletePermission(int id)
        {
            return Ok(await _permissionService.DeletePermissionAsync(id));
        }
    }
}
