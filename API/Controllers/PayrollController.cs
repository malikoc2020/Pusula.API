using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.DTO;
using Services.Services.PayrollService;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class PayrollController : ControllerBase
    {
        private readonly ILogger<PayrollController> _logger;
        private readonly IPayrollService _payrollService;

        public PayrollController(ILogger<PayrollController> logger, IPayrollService payrollService)
        {
            _logger = logger;
            _payrollService = payrollService;
        }


        [HttpGet("GetAllPayrollSettings")]
        public async Task<IActionResult> GetAllPayrollSettings()
        {
            return Ok(await _payrollService.GetAllPayrollSettingsAsync());
        }
        [HttpGet("GetPayrollSettingById/{id}")]
        public async Task<ActionResult> GetPayrollSettingById(int id)
        {
            return Ok(await _payrollService.GetPayrollSettingByIdAsync(id));
        }
        [HttpPost("InsertPayrollSetting")]
        public async Task<IActionResult> InsertPayrollSetting([FromBody] PayrollSettingDTO request)
        {
            if (ModelState.IsValid)
            {
                var result = await _payrollService.CreatePayrollSettingAsync(request);
                return Ok(result);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        [HttpPost("UpdatePayrollSetting")]
        public async Task<IActionResult> UpdatePayrollSetting([FromBody] PayrollSettingDTO request)
        {
            if (ModelState.IsValid)
            {
                var result = await _payrollService.UpdatePayrollSettingAsync(request);
                return Ok(result);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        [HttpDelete("DeletePayrollSetting/{id}")]
        public async Task<ActionResult> DeletePayrollSetting(int id)
        {
            return Ok(await _payrollService.DeletePayrollSettingAsync(id));
        }



        [HttpGet("GetAllPayrolls")]
        public async Task<IActionResult> GetAllPayrolls([FromQuery]PayrollFilterDTO request)
        {
            return Ok(await _payrollService.GetAllPayrollsAsync(request));
        }
        [HttpGet("GetPayrollById/{id}")]
        public async Task<ActionResult> GetPayrollById(int id)
        {
            return Ok(await _payrollService.GetPayrollByIdAsync(id));
        }
        [HttpPost("InsertPayroll")]
        public async Task<IActionResult> InsertPayroll([FromBody] PayrollDTO request)
        {
            if (ModelState.IsValid)
            {
                var result = await _payrollService.CreatePayrollAsync(request);
                return Ok(result);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        [HttpPost("UpdatePayroll")]
        public async Task<IActionResult> UpdatePayroll([FromBody] PayrollDTO request)
        {
            if (ModelState.IsValid)
            {
                var result = await _payrollService.UpdatePayrollAsync(request);
                return Ok(result);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        [HttpDelete("DeletePayroll/{id}")]
        public async Task<ActionResult> DeletePayroll(int id)
        {
            return Ok(await _payrollService.DeletePayrollAsync(id));
        }


        [HttpGet("GetAllPayrollTemps")]
        public async Task<IActionResult> GetAllPayrollTemps([FromQuery] PayrollTempFilterDTO request)
        {
            return Ok(await _payrollService.GetAllPayrollTempsAsync(request));
        }
        [HttpGet("GetPayrollTempById/{id}")]
        public async Task<ActionResult> GetPayrollTempById(int id)
        {
            return Ok(await _payrollService.GetPayrollTempByIdAsync(id));
        }
        [HttpPost("InsertPayrollTemp")]
        public async Task<IActionResult> InsertPayrollTemp([FromBody] PayrollTempDTO request)
        {
            if (ModelState.IsValid)
            {
                var result = await _payrollService.CreatePayrollTempAsync(request);
                return Ok(result);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        [HttpPost("UpdatePayrollTemp")]
        public async Task<IActionResult> UpdatePayrollTemp([FromBody] PayrollTempDTO request)
        {
            if (ModelState.IsValid)
            {
                var result = await _payrollService.UpdatePayrollTempAsync(request);
                return Ok(result);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        [HttpDelete("DeletePayrollTemp/{id}")]
        public async Task<ActionResult> DeletePayrollTemp(int id)
        {
            return Ok(await _payrollService.DeletePayrollTempAsync(id));
        }
    }
}
