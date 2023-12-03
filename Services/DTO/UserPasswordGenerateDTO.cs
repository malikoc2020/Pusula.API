namespace Services.DTO
{
    public class UserPasswordGenerateDTO
    {
        public UserPasswordGenerateDTO(string Name, string SurName, string Email, string PhoneNumber)
        {
            this.Name = Name;
            this.SurName = SurName;
            this.Email = Email;
            this.PhoneNumber = PhoneNumber;
        }
        public string Name { get; set; } = "";
        public string SurName { get; set; } = "";
        public string? Email { get; set; } = "";
        public string PhoneNumber { get; set; } = "";
    }
}
