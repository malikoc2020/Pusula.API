using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.DTO;
using Services.Services.WorksiteService;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class WorksiteController : ControllerBase
    {
        private readonly ILogger<WorksiteController> _logger;
        private readonly IWorksiteService _worksiteService;
        public WorksiteController(ILogger<WorksiteController> logger, IWorksiteService worksiteService)
        {
            _logger = logger;
            _worksiteService = worksiteService;
        }

        [HttpGet("GetAllWorksites")]
        public async Task<IActionResult> GetAllWorksites()
        {
            return Ok(await _worksiteService.GetAllWorksitesAsync());
        }

        [HttpGet("GetWorksiteById/{id}")]
        public async Task<ActionResult> GetWorksiteById(int id)
        {
            return Ok(await _worksiteService.GetWorksiteByIdAsync(id));
        }
        [HttpPost("InsertWorksite")]
        public async Task<IActionResult> InsertWorksite([FromBody] WorksiteDTO request)
        {
            if (ModelState.IsValid)
            {
                var result = await _worksiteService.CreateWorksiteAsync(request);
                return Ok(result);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        [HttpPost("UpdateWorksite")]
        public async Task<IActionResult> UpdateWorksite([FromBody] WorksiteDTO request)
        {
            if (ModelState.IsValid)
            {
                var result = await _worksiteService.UpdateWorksiteAsync(request);
                return Ok(result);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        [HttpGet("GetAllWorksiteWorkerTypes")]
        public async Task<IActionResult> GetAllWorksiteWorkerTypes()
        {
            return Ok(await _worksiteService.GetAllWorksiteWorkerTypesAsync());
        }
    }
}
