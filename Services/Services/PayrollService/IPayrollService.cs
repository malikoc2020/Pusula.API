using Domain.Entities;
using Services.DTO;
using Services.Response;

namespace Services.Services.PayrollService
{
    public interface IPayrollService
    {
        Task<BaseResponse> GetAllPayrollSettingsAsync();
        Task<BaseResponse> GetPayrollSettingByIdAsync(int id);
        Task<BaseResponse> CreatePayrollSettingAsync(PayrollSettingDTO request);
        Task<BaseResponse> UpdatePayrollSettingAsync(PayrollSettingDTO request);
        Task<BaseResponse> DeletePayrollSettingAsync(int id);

        Task<BaseResponse> GetAllPayrollsAsync();
        Task<BaseResponse> GetPayrollByIdAsync(int id);
        Task<BaseResponse> CreatePayrollAsync(PayrollDTO request);
        Task<BaseResponse> UpdatePayrollAsync(PayrollDTO request);
        Task<BaseResponse> DeletePayrollAsync(int id);

        Task<BaseResponse> GetAllPayrollTempsAsync();
        Task<BaseResponse> GetPayrollTempByIdAsync(int id);
        Task<BaseResponse> CreatePayrollTempAsync(PayrollTempDTO request);
        Task<BaseResponse> UpdatePayrollTempAsync(PayrollTempDTO request);
        Task<BaseResponse> DeletePayrollTempAsync(int id);
    }
}
