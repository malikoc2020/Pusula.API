using Domain.Base;

namespace Domain.Entities
{
    public class WorksiteAction:BaseEntity
    {
        public int WorksiteId { get; set; }
        public int WorksiteActionTypeId { get; set; }
        public DateTime Date { get; set; }
        public string Value { get; set; }
        public virtual Worksite Worksite { get; set; }
        public virtual WorksiteActionType WorksiteActionType { get; set; }

    }
}
