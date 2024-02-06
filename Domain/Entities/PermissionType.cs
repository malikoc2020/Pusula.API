using Domain.Base;

namespace Domain.Entities
{
    public class PermissionType:BaseEntity
    {
        public string Name { get; set; } = "";
        public virtual ICollection<Permission> Permissions { get; set; }

    }
}
