using Domain.Entities;
using Services.DTO;
using Services.Response;

namespace Services.Services.WorksiteService
{
    public interface IWorksiteService
    {
        Task<BaseResponse> GetAllWorksitesAsync();
        Task<BaseResponse> GetWorksiteByIdAsync(int id);
        Task<BaseResponse> CreateWorksiteAsync(WorksiteDTO request);
        Task<BaseResponse> UpdateWorksiteAsync(WorksiteDTO request);
        Task<BaseResponse> DeleteWorksiteAsync(int id);
        Task<BaseResponse> GetAllWorksiteWorkerTypesAsync();
    }
}
