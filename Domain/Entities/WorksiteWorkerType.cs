using Domain.Base;

namespace Domain.Entities
{
    public class WorksiteWorkerType:BaseEntity
    {
        public string Name { get; set; }
        public decimal OvertimeWage { get; set; }
        public virtual ICollection<WorksiteWorker> WorksiteWorkers { get; set; }

    }
}
