namespace Domain.Base
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public int CreatedBy { get; set; } = 1;
        public DateTime UpdatedAt { get; set; }
        public int UpdatedBy { get; set; }
    }
}
