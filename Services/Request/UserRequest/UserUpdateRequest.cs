using System.ComponentModel.DataAnnotations;

namespace Services.Request.UserRequest
{
    public class UserUpdateRequest
    {
        public string Id { get; set; } = "";
        [Required]
        public string Name { get; set; } = "";
        [Required]
        public string SurName { get; set; } = "";
        [Required]
        public string Email { get; set; } = "";
        [Required]
        public string PhoneNumber { get; set; } = "";
        public string UserId { get; set; } = "";
        public List<string> UserRoles { get; set; } = new List<string>();
    }
}
