using Domain.Base;

namespace Domain.Entities
{
    public class Permission:BaseEntity
    {
        public string UserId { get; set; }
        public int PermissionTypeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public virtual User User { get; set; }
        public virtual PermissionType PermissionType { get; set; }
    }
}
