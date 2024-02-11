using Domain.Entities;
using Services.DTO;
using Services.Response;

namespace Services.Services.PermissionService
{
    public interface IPermissionService
    {
        Task<BaseResponse> GetAllPermissionsAsync();
        Task<BaseResponse> GetPermissionByIdAsync(int id);
        Task<BaseResponse> CreatePermissionAsync(PermissionDTO request);
        Task<BaseResponse> UpdatePermissionAsync(PermissionDTO request);
        Task<BaseResponse> DeletePermissionAsync(int id);
        Task<BaseResponse> GetAllPermissionTypesAsync();
    }
}
