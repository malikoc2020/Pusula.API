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
        private readonly IRepository<WorksiteWorker> _WorksiteWorkerRepository;
        private readonly IRepository<WorksiteActionType> _WorksiteActionTypeRepository;
        private readonly IRepository<WorksiteAction> _WorksiteActionRepository;
        private readonly IUnitOfWork _unitOfWork;


        public WorksiteService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _WorksiteRepository = _unitOfWork.GetRepository<Worksite>();
            _WorksiteWorkerTypeRepository = _unitOfWork.GetRepository<WorksiteWorkerType>();
            _WorksiteWorkerRepository = _unitOfWork.GetRepository<WorksiteWorker>();
            _WorksiteActionTypeRepository = _unitOfWork.GetRepository<WorksiteActionType>();
            _WorksiteActionRepository = _unitOfWork.GetRepository<WorksiteAction>();
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

        #region WorksiteWorker

        public async Task<BaseResponse> GetAllWorksiteWorkerTypesAsync()
        {
            var worksites = await _WorksiteWorkerTypeRepository.GetAllAsQueryable().Select(x => new WorksiteWorkerTypeDTO()
            {
                Id = x.Id,
                Name = x.Name
            }).ToListAsync();
            return new BaseResponse(true, "", worksites);
        }
        public async Task<BaseResponse> GetWorksiteWorkerByIdAsync(int id)
        {
            var entity = await _WorksiteWorkerRepository.GetByIdAsync(id);
            if (entity is null)
            {
                return new BaseResponse(false, "Worksite Worker Could Not Found", null);
            }

            var entityDTO = new WorksiteWorkerDTO()
            {
                Id = entity.Id,
                WorksiteId = entity.WorksiteId,
                WorksiteWorkerTypeId = entity.WorksiteWorkerTypeId,
                UserId = entity.UserId,
                StartDate = entity.StartDate,
                EndDate = entity.EndDate
            };
            return new BaseResponse(true, "", entityDTO);

        }
        public async Task<BaseResponse> GetWorksiteWorkersByWorksiteId(int id)
        {
            var worksiteWorkers = await _WorksiteWorkerRepository.GetAllAsQueryable().Include(x => x.WorksiteWorkerType).Include(x => x.User).Where(x=>x.WorksiteId == id).Select(x => new WorksiteWorkerDTO()
            {
                Id = x.Id,
                UserId = x.UserId,
                WorksiteId = x.WorksiteId,
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                UserName = x.User.Name + " " + x.User.UserName,
                WorksiteWorkerTypeName = x.WorksiteWorkerType.Name
            }).OrderBy(x=>x.StartDate).ThenBy(x=>x.EndDate).ToListAsync();
            return new BaseResponse(true, "", worksiteWorkers);

        }
        public async Task<BaseResponse> CreateWorksiteWorkerAsync(WorksiteWorkerDTO request)
        {
            var worksiteWorker = new WorksiteWorker() { WorksiteId = request.WorksiteId, WorksiteWorkerTypeId = request.WorksiteWorkerTypeId, UserId = request.UserId, StartDate = request.StartDate, EndDate = request.EndDate };
            await _WorksiteWorkerRepository.AddAsync(worksiteWorker);
            await _unitOfWork.CommitAsync();
            return new BaseResponse(true, "", new WorksiteWorkerDTO() { Id = worksiteWorker.Id, UserId = worksiteWorker.UserId, WorksiteWorkerTypeId = worksiteWorker.WorksiteWorkerTypeId, StartDate = worksiteWorker.StartDate, EndDate = worksiteWorker.EndDate });
        }

        public async Task<BaseResponse> UpdateWorksiteWorkerAsync(WorksiteWorkerDTO request)
        {
            var entity = await _WorksiteWorkerRepository.GetByIdAsync(request.Id);
            if (entity is null)
            {
                return new BaseResponse(false, "WorksiteWorker not found!", null);

            }
            entity.UserId = request.UserId;
            entity.WorksiteId = request.WorksiteId;
            entity.WorksiteWorkerTypeId = request.WorksiteWorkerTypeId;
            entity.StartDate = request.StartDate;
            entity.EndDate = request.EndDate;

            await _WorksiteWorkerRepository.UpdateAsync(entity);
            await _unitOfWork.CommitAsync();
            return new BaseResponse(true, "", new WorksiteWorkerDTO() { Id = entity.Id, UserId = entity.UserId, WorksiteId = entity.WorksiteId, WorksiteWorkerTypeId = entity.WorksiteWorkerTypeId, StartDate = entity.StartDate, EndDate = entity.EndDate });
        }

        public async Task<BaseResponse> DeleteWorksiteWorkerAsync(int id)
        {
            var entity = await _WorksiteWorkerRepository.GetByIdAsync(id);
            if (entity is not null)
            {
                await _WorksiteWorkerRepository.DeleteAsync(entity);
                await _unitOfWork.CommitAsync();
                return new BaseResponse(true, "", null);
            }
            return new BaseResponse(false, "Worksite Worker Could Not Found", null);
        }

        #endregion






        #region WorksiteAction

        public async Task<BaseResponse> GetAllWorksiteActionTypesAsync()
        {
            var worksites = await _WorksiteActionTypeRepository.GetAllAsQueryable().Select(x => new WorksiteActionTypeDTO()
            {
                Id = x.Id,
                Name = x.Name
            }).ToListAsync();
            return new BaseResponse(true, "", worksites);
        }
        public async Task<BaseResponse> GetWorksiteActionByIdAsync(int id)
        {
            var entity = await _WorksiteActionRepository.GetByIdAsync(id);
            if (entity is null)
            {
                return new BaseResponse(false, "Worksite Action Could Not Found", null);
            }

            var entityDTO = new WorksiteActionDTO()
            {
                Id = entity.Id,
                WorksiteId = entity.WorksiteId,
                WorksiteActionTypeId = entity.WorksiteActionTypeId,
                Date = entity.Date,
                Value = entity.Value

            };
            return new BaseResponse(true, "", entityDTO);

        }
        public async Task<BaseResponse> GetWorksiteActionsByWorksiteId(int id)
        {
            var worksiteActions = await _WorksiteActionRepository.GetAllAsQueryable().Include(x => x.WorksiteActionType).Where(x => x.WorksiteId == id).Select(x => new WorksiteActionDTO()
            {
                Id = x.Id,
                WorksiteId = x.WorksiteId,
                Date = x.Date,
                Value = x.Value,
                WorksiteActionTypeId = x.WorksiteActionTypeId,
                WorksiteActionTypeName = x.WorksiteActionType.Name
            }).OrderBy(x => x.Date).ThenBy(x => x.WorksiteActionTypeId).ToListAsync();
            return new BaseResponse(true, "", worksiteActions);

        }
        public async Task<BaseResponse> CreateWorksiteActionAsync(WorksiteActionDTO request)
        {
            var worksiteAction = new WorksiteAction() { WorksiteId = request.WorksiteId, WorksiteActionTypeId = request.WorksiteActionTypeId, Date = request.Date, Value = request.Value };
            await _WorksiteActionRepository.AddAsync(worksiteAction);
            await _unitOfWork.CommitAsync();
            return new BaseResponse(true, "", new WorksiteActionDTO() { Id = worksiteAction.Id, WorksiteActionTypeId = worksiteAction.WorksiteActionTypeId, Date = worksiteAction.Date, Value = worksiteAction.Value });
        }

        public async Task<BaseResponse> UpdateWorksiteActionAsync(WorksiteActionDTO request)
        {
            var entity = await _WorksiteActionRepository.GetByIdAsync(request.Id);
            if (entity is null)
            {
                return new BaseResponse(false, "WorksiteAction not found!", null);

            }
            entity.WorksiteId = request.WorksiteId;
            entity.WorksiteActionTypeId = request.WorksiteActionTypeId;
            entity.Date = request.Date;
            entity.Value = request.Value;


            await _WorksiteActionRepository.UpdateAsync(entity);
            await _unitOfWork.CommitAsync();
            return new BaseResponse(true, "", new WorksiteActionDTO() { Id = entity.Id, WorksiteId = entity.WorksiteId, WorksiteActionTypeId = entity.WorksiteActionTypeId, Date = entity.Date, Value = entity.Value });
        }

        public async Task<BaseResponse> DeleteWorksiteActionAsync(int id)
        {
            var entity = await _WorksiteActionRepository.GetByIdAsync(id);
            if (entity is not null)
            {
                await _WorksiteActionRepository.DeleteAsync(entity);
                await _unitOfWork.CommitAsync();
                return new BaseResponse(true, "", null);
            }
            return new BaseResponse(false, "Worksite Action Could Not Found", null);
        }
        #endregion
    }
}
