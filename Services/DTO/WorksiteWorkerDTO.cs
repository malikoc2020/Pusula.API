using Domain.Entities;

namespace Services.DTO
{
    public class WorksiteWorkerDTO
    {
        public int Id { get; set; }
        public int WorkersiteId { get; set; }
        public string UserId { get; set; } = string.Empty;
        public int WorksiteWorkerTypeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string WorksiteWorkerTypeName { get; set; } = string.Empty;

    }
}
