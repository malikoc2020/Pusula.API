using DAL.Repository;
using DAL.UnitOfWork;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Services.DTO;
using Services.Response;

namespace Services.Services.WorksiteService
{
    public class WorksiteService : IWorksiteService
    {
        private readonly IRepository<Worksite> _WorksiteRepository;
        private readonly IRepository<WorksiteWorkerType> _WorksiteWorkerTypeRepository;
        private readonly IUnitOfWork _unitOfWork;


        public WorksiteService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _WorksiteRepository = _unitOfWork.GetRepository<Worksite>();
            _WorksiteWorkerTypeRepository = _unitOfWork.GetRepository<WorksiteWorkerType>();
        }

        public async Task<BaseResponse> GetAllWorksitesAsync()
        {
            var worksites = await _WorksiteRepository.GetAllAsQueryable().Include(x=>x.il).Include(x=>x.ilce).Select(x=>new WorksiteDTO() {
            Id = x.Id,
            Name = x.Name,
            Description = x.Description,
            StartDate = x.StartDate,
            EndDate = x.EndDate,
            ilId = x.ilId,
            ilAd = x.il.Name,
            ilceId = x.ilceId,
            ilceAd = x.ilce.Name
            }).ToListAsync();
            return new BaseResponse(true, "", worksites);
        }

        public async Task<BaseResponse> GetWorksiteByIdAsync(int id)
        {
            var Worksite =  await _WorksiteRepository.GetByIdAsync(id);
            if (Worksite is null)
            {
                return new BaseResponse(false,"Worksite Could Not Found",null);
            }

            var worksiteDTO = new WorksiteDTO()
            {
                Id = Worksite.Id,
                Name= Worksite.Name,
                Description = Worksite.Description,
                ilId= Worksite.ilId,
                ilceId = Worksite.ilceId,
                StartDate = Worksite.StartDate,
                EndDate = Worksite.EndDate
            };
            return new BaseResponse(true, "",Worksite);

        }
        public async Task<BaseResponse> CreateWorksiteAsync(WorksiteDTO request)
        {
            var worksite  = new Worksite() { Name = request.Name, Description = request.Description, ilId = request.ilId, ilceId = request.ilceId, StartDate = request.StartDate, EndDate = request.EndDate };
            await _WorksiteRepository.AddAsync(worksite);
            await _unitOfWork.CommitAsync();
            return new BaseResponse(true, "", new WorksiteDTO() { Id = worksite.Id, Name = worksite.Name, Description = worksite.Description, ilId = worksite.ilId, ilceId = worksite.ilceId, StartDate = worksite.StartDate, EndDate = worksite.EndDate});
        }

        public async Task<BaseResponse> UpdateWorksiteAsync(WorksiteDTO request)
        {
            var worksite = await _WorksiteRepository.GetByIdAsync(request.Id);
            if (worksite is null)
            {
                return new BaseResponse(false, "Worksite not found!", null);

            }
            worksite.Name = request.Name;
            worksite.Description = request.Description;
            worksite.ilId = request.ilId;
            worksite.ilceId = request.ilceId;
            worksite.StartDate = request.StartDate;   
            worksite.EndDate = request.EndDate;            

            await _WorksiteRepository.UpdateAsync(worksite);
            await _unitOfWork.CommitAsync();
            return new BaseResponse(true, "", new WorksiteDTO() { Id = worksite.Id, Name = worksite.Name, Description = worksite.Description, ilId = worksite.ilId, ilceId = worksite.ilceId, StartDate = worksite.StartDate, EndDate = worksite.EndDate });
        }

        public async Task<BaseResponse> DeleteWorksiteAsync(int id)
        {
            var Worksite = await _WorksiteRepository.GetByIdAsync(id);
            if (Worksite is not null)
            {
                await _WorksiteRepository.DeleteAsync(Worksite);
                await _unitOfWork.CommitAsync();
                return new BaseResponse(true,"",null);
            }
            return new BaseResponse(false, "Worksite Type Could Not Found", null);
        }
        public async Task<BaseResponse> GetAllWorksiteWorkerTypesAsync()
        {
            var worksites = await _WorksiteWorkerTypeRepository.GetAllAsQueryable().Select(x => new WorksiteWorkerTypeDTO()
            {
                Id = x.Id,
                Name = x.Name
            }).ToListAsync();
            return new BaseResponse(true, "", worksites);
        }
    }
}
