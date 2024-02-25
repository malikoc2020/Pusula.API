using Domain.Base;

namespace Domain.Entities
{
    public class Worksite:BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int ilId { get; set; }
        public int ilceId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public virtual il il { get; set; }
        public virtual ilce ilce { get; set; }
        public virtual ICollection<WorksiteWorker> WorksiteWorkers { get; set; }


    }
}
