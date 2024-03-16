using Domain.Base;

namespace Domain.Entities
{
    public class PayrollSetting:BaseEntity
    {
        public int YearId { get; set; }
        public int MonthId { get; set; }
        public bool IsApproved { get; set; }
        public virtual Year Year { get; set; }
        public virtual Month Month { get; set; }
    }
}
