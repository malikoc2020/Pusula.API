using Domain.Base;

namespace Domain.Entities
{
    public class Role : BaseEntity
    {
        public string Name { get; set; } = "";
        public string? ConcurrencyStamp { get; set; } = "";

        public ICollection<UserRole> UserRoles { get; set; }
    }
}
