namespace Services.DTO
{
    public class PermissionDTO
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public int PermissionTypeId { get; set; }
        public string PermissionTypeName { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Days
        {
            get
            {
                return (EndDate - StartDate).Days+1;
            }
        }
    }
}
