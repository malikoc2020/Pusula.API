using Domain.Base;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Role : IdentityRole
    {
        public Role() { }

        public Role(string roleName)
        {
            Name = roleName;
        }

        //public string Name { get; set; } = "";
        //public string? ConcurrencyStamp { get; set; } = "";
        [Key]
        public override string Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public int CreatedBy { get; set; } = 1;
        public DateTime UpdatedAt { get; set; }
        public int UpdatedBy { get; set; }
    }
}
