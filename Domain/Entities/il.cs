using Domain.Base;

namespace Domain.Entities
{
    public class il:BaseEntity
    {
        public string Name { get; set; }
        public virtual ICollection<ilce> ilces { get; set; }

    }
}
