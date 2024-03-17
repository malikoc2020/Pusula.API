using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Services.CommonService;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class CommonController : ControllerBase
    {
        private readonly ILogger<CommonController> _logger;
        private readonly ICommonService _commonService;
        public CommonController(ILogger<CommonController> logger, ICommonService commonService)
        {
            _logger = logger;
            _commonService = commonService;
        }

        [HttpGet("GetAllProvinces")]
        public async Task<IActionResult> GetAllProvinces()
        {
            return Ok(await _commonService.GetAllProvinces());
        }
        [HttpGet("GetAllDistricts")]
        public async Task<IActionResult> GetAllDistricts()
        {
            return Ok(await _commonService.GetAllDistricts());
        }
        [HttpGet("GetAllYears")]
        public async Task<IActionResult> GetAllYears()
        {
            return Ok(await _commonService.GetAllYears());
        }
        [HttpGet("GetAllMonths")]
        public async Task<IActionResult> GetAllMonths()
        {
            return Ok(await _commonService.GetAllMonths());
        }
    }
}
