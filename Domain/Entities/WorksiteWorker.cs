using Domain.Base;

namespace Domain.Entities
{
    public class WorksiteWorker:BaseEntity
    {
        public int WorkersiteId { get; set; }
        public string UserId { get; set; }
        public int WorksiteWorkerTypeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public virtual Worksite Worksite { get; set; }
        public virtual User User { get; set; }
        public virtual WorksiteWorkerType WorksiteWorkerType { get; set; }

    }
}
