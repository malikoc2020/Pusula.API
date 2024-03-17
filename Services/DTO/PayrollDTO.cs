using Domain.Base;

namespace Domain.Entities
{
    public class PayrollDTO
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public int YearId { get; set; }
        public int MonthId { get; set; }
        public string MonthName { get; set; }
        public decimal Salary { get; set; }
        public decimal Overtime { get; set; }
    }
}
