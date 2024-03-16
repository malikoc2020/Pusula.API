using Domain.Base;

namespace Domain.Entities
{
    public class Month:BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public virtual ICollection<PayrollSetting> PayrollSettings { get; set; }
        public virtual ICollection<Payroll> Payrolls { get; set; }
        public virtual ICollection<PayrollTemp> PayrollTemps { get; set; }

    }
}
