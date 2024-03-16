namespace Services.DTO
{
    public class UserDTO
    {
        public UserDTO()
        {
            permissions = new List<PermissionDTO>();    
        }
        public string Id { get; set; } = "";
        public string Name { get; set; } = "";
        public string SurName { get; set; } = "";
        public string? Email { get; set; } = "";
        public bool? EmailConfirmed { get; set; } = false;
        public string PhoneNumber { get; set; } = "";
        public bool PhoneNumberConfirmed { get; set; } = false;
        public string? SecurityStamp { get; set; } = "";
        public string? ConcurrencyStamp { get; set; } = "";
        public bool? TwoFactorEnabled { get; set; } = false;
        public DateTimeOffset? LockoutEnd { get; set; }
        public bool? LockoutEnabled { get; set; } = false;
        public int? AccessFailedCount { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public int CreatedBy { get; set; } = 1;
        public DateTime UpdatedAt { get; set; }
        public int UpdatedBy { get; set; }
        public string Token { get; set; } = "";
        public List<string> UserRoles { get; set; } = new List<string>();
        public DateTime? DateOfStart { get; set; }
        public decimal Salary { get; set; }
        public List<PermissionDTO> permissions { get; set; }
        public int AnnualLeaveDaysToUse
        {
            get
            {
                if (DateOfStart.HasValue)
                {
                    return (DateTime.Now - DateOfStart.Value).Days/7 - permissions.Sum(x => x.Days);
                }
                else
                {
                    return 0;
                }
            }
        }

    }
}
