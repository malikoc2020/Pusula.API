using Domain.Base;

namespace Domain.Entities
{
    public class WorksiteWorker:BaseEntity
    {
        public string UserId { get; set; }
        public int WorkSiteWorkeTypeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public virtual User User { get; set; }
        public virtual Worksite Worksite { get; set; }
        public virtual WorksiteWorkerType WorksiteWorkeType { get; set; }

    }
}
