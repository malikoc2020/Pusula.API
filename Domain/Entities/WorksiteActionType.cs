using Domain.Base;

namespace Domain.Entities
{
    public class WorksiteActionType:BaseEntity
    {
        public string Name { get; set; }
        public virtual ICollection<WorksiteAction> WorksiteActions { get; set; }

    }
}
