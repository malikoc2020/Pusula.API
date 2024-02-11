using DAL.Repository;
using DAL.UnitOfWork;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Services.DTO;
using Services.Response;

namespace Services.Services.PermissionService
{
    public class PermissionService : IPermissionService
    {
        private readonly IRepository<Permission> _PermissionRepository;
        private readonly IRepository<PermissionType> _PermissionTypeRepository;
        private readonly IUnitOfWork _unitOfWork;


        public PermissionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _PermissionRepository = _unitOfWork.GetRepository<Permission>();
            _PermissionTypeRepository = _unitOfWork.GetRepository<PermissionType>();
        }

        public async Task<BaseResponse> GetAllPermissionsAsync()
        {
            var permissions = await _PermissionRepository.GetAllAsQueryable().Include(x=>x.User).Include(x=>x.PermissionType).Select(x=>new PermissionDTO() {
            Id = x.Id,
            PermissionTypeId = x.PermissionTypeId,
            StartDate = x.StartDate,
            EndDate = x.EndDate,
            UserId  = x.UserId,
            UserName = x.User.Name + " " + x.User.SurName,
            PermissionTypeName = x.PermissionType.Name
            }).ToListAsync();
            return new BaseResponse(true, "", permissions);
        }

        public async Task<BaseResponse> GetPermissionByIdAsync(int id)
        {
            var Permission =  await _PermissionRepository.GetByIdAsync(id);
            if (Permission is null)
            {
                return new BaseResponse(false,"Permission Could Not Found",null);
            }

            var permissionDTO = new PermissionDTO()
            {
                Id = Permission.Id,
                UserId = Permission.UserId,
                PermissionTypeId = Permission.PermissionTypeId,
                StartDate = Permission.StartDate,
                EndDate = Permission.EndDate
            };
            return new BaseResponse(true, "",Permission);

        }
        public async Task<BaseResponse> CreatePermissionAsync(PermissionDTO request)
        {
            var permission  = new Permission() { UserId = request.UserId, PermissionTypeId = request.PermissionTypeId, StartDate = request.StartDate, EndDate = request.EndDate };
            await _PermissionRepository.AddAsync(permission);
            await _unitOfWork.CommitAsync();
            return new BaseResponse(true, "", new PermissionDTO() { Id = permission.Id, UserId = permission.UserId, PermissionTypeId = permission.PermissionTypeId, StartDate = permission.StartDate, EndDate = permission.EndDate});
        }

        public async Task<BaseResponse> UpdatePermissionAsync(PermissionDTO request)
        {
            var permission = await _PermissionRepository.GetByIdAsync(request.Id);
            if (permission is null)
            {
                return new BaseResponse(false, "Permission not found!", null);

            }
            permission.StartDate = request.StartDate;   
            permission.EndDate = request.EndDate;
            permission.UserId = request.UserId;
            permission.PermissionTypeId = request.PermissionTypeId;

            await _PermissionRepository.UpdateAsync(permission);
            await _unitOfWork.CommitAsync();
            return new BaseResponse(true, "", new PermissionDTO() { Id = permission.Id, UserId = permission.UserId, PermissionTypeId = permission.PermissionTypeId, StartDate = permission.StartDate, EndDate = permission.EndDate });
        }

        public async Task<BaseResponse> DeletePermissionAsync(int id)
        {
            var Permission = await _PermissionRepository.GetByIdAsync(id);
            if (Permission is not null)
            {
                await _PermissionRepository.DeleteAsync(Permission);
                await _unitOfWork.CommitAsync();
                return new BaseResponse(true,"",null);
            }
            return new BaseResponse(false, "Permission Type Could Not Found", null);
        }
        public async Task<BaseResponse> GetAllPermissionTypesAsync()
        {
            var permissions = await _PermissionTypeRepository.GetAllAsQueryable().Select(x => new PermissionTypeDTO()
            {
                Id = x.Id,
                Name = x.Name
            }).ToListAsync();
            return new BaseResponse(true, "", permissions);
        }
    }
}
