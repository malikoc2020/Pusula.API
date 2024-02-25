using Domain.Base;

namespace Domain.Entities
{
    public class ilce:BaseEntity
    {
        public string Name { get; set; }
        public int ilId { get; set; }
        public virtual il il { get; set; }

    }
}
