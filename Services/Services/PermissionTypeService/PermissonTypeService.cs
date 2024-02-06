using DAL.Repository;
using DAL.UnitOfWork;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Services.DTO;
using Services.Response;

namespace Services.Services.PermissionTypeService
{
    public class PermissionTypeService : IPermissionTypeService
    {
        private readonly IRepository<PermissionType> _PermissionTypeRepository;
        private readonly IUnitOfWork _unitOfWork;


        public PermissionTypeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _PermissionTypeRepository = _unitOfWork.GetRepository<PermissionType>();
        }

        public async Task<BaseResponse> GetAllPermissionTypesAsync()
        {
            var PermissionTypes = await _PermissionTypeRepository.GetAllAsync();
            return new BaseResponse(true, "", PermissionTypes);
        }

        public async Task<BaseResponse> GetPermissionTypeByIdAsync(int id)
        {
            var PermissionType =  await _PermissionTypeRepository.GetByIdAsync(id);
            if (PermissionType is null)
            {
                return new BaseResponse(false,"Permission Type Could Not Found",null);
            }

            var permissionTypeDTO = new PermissionTypeDTO()
            {
                Id = PermissionType.Id,
                Name = PermissionType.Name
            };
            return new BaseResponse(true, "",PermissionType);

        }
        public async Task<BaseResponse> CreatePermissionTypeAsync(PermissionType permissionType)
        {
            await _PermissionTypeRepository.AddAsync(permissionType);
            await _unitOfWork.CommitAsync();
            return new BaseResponse(true, "", new PermissionTypeDTO() { Id = permissionType.Id, Name = permissionType.Name});
        }

        public async Task<BaseResponse> UpdatePermissionTypeAsync(PermissionType permissionType)
        {
            await _PermissionTypeRepository.UpdateAsync(permissionType);
            await _unitOfWork.CommitAsync();
            return new BaseResponse(true, "", new PermissionTypeDTO() { Id = permissionType.Id, Name = permissionType.Name });
        }

        public async Task<BaseResponse> DeletePermissionTypeAsync(int id)
        {
            var PermissionType = await _PermissionTypeRepository.GetByIdAsync(id);
            if (PermissionType is not null)
            {
                await _PermissionTypeRepository.DeleteAsync(PermissionType);
                await _unitOfWork.CommitAsync();
                return new BaseResponse(true,"",null);
            }
            return new BaseResponse(false, "Permission Type Could Not Found", null);
        }

    }
}
