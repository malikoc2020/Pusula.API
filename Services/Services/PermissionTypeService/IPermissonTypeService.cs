using Domain.Entities;
using Services.Response;

namespace Services.Services.PermissionTypeService
{
    public interface IPermissionTypeService
    {
        Task<BaseResponse> GetAllPermissionTypesAsync();
        Task<BaseResponse> GetPermissionTypeByIdAsync(int id);
        Task<BaseResponse> CreatePermissionTypeAsync(PermissionType permissionType);
        Task<BaseResponse> UpdatePermissionTypeAsync(PermissionType permissionType);
        Task<BaseResponse> DeletePermissionTypeAsync(int id);
    }
}
