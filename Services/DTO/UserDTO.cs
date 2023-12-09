namespace Services.DTO
{
    public class UserDTO
    {
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
    }
}
