using Domain.Base;

namespace Domain.Entities
{
    public class PayrollSettingDTO
    {
        public int Id { get; set; }
        public int YearId { get; set; }
        public int MonthId { get; set; }
        public string MonthName { get; set; }=string.Empty;
        public bool IsApproved { get; set; }
    }
}
