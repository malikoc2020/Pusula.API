using Domain.Base;
namespace Domain.Entities
{
    public class PayrollTemp : BaseEntity
    {
        public string UserId { get; set; }
        public int YearId { get; set; }
        public int MonthId { get; set; }
        public decimal Salary { get; set; }
        public decimal Overtime { get; set; }
        public virtual User User { get; set; }
        public virtual Year Year { get; set; }
        public virtual Month Month { get; set; }
    }
}
