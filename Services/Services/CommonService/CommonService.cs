using DAL.Repository;
using DAL.UnitOfWork;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Services.DTO;
using Services.Response;

namespace Services.Services.CommonService
{
    public class CommonService : ICommonService
    {
        private readonly IRepository<il> _ilRepository;
        private readonly IRepository<ilce> _ilceRepository;
        private readonly IUnitOfWork _unitOfWork;


        public CommonService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _ilRepository = _unitOfWork.GetRepository<il>();
            _ilceRepository = _unitOfWork.GetRepository<ilce>();
        }

        public async Task<BaseResponse> GetAllProvinces()
        {
            var worksites = await _ilRepository.GetAllAsQueryable().Select(x => new ProvinceDTO()
            {
                Id = x.Id,
                Name = x.Name
            }).ToListAsync();
            return new BaseResponse(true, "", worksites);
        }
        public async Task<BaseResponse> GetAllDistricts()
        {
            var worksites = await _ilceRepository.GetAllAsQueryable().Select(x => new DistrictDTO()
            {
                Id = x.Id,
                Name = x.Name,
                ilId = x.ilId,
            }).ToListAsync();
            return new BaseResponse(true, "", worksites);
        }
    }
}
