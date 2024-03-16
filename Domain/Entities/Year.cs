using Domain.Base;

namespace Domain.Entities
{
    public class Year:BaseEntity
    {
        public virtual ICollection<PayrollSetting> PayrollSettings { get; set; }
        public virtual ICollection<Payroll> Payrolls { get; set; }
        public virtual ICollection<PayrollTemp> PayrollTemps { get; set; }
    }
}
