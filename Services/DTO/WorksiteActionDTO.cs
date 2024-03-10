using Domain.Entities;

namespace Services.DTO
{
    public class WorksiteActionDTO
    {
        public int Id { get; set; }
        public int WorksiteId { get; set; }
        public int WorksiteActionTypeId { get; set; }
        public DateTime Date { get; set; }
        public string Value { get; set; } = string.Empty;
        public string WorksiteActionTypeName { get; set; } = string.Empty;

    }
}
