using Domain.Entities;

namespace Services.DTO
{
    public class WorksiteDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ilId { get; set; }
        public string ilAd { get; set; }
        public int ilceId { get; set; }
        public string ilceAd { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
