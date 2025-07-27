namespace Deskstones.LMS.Infrastructure.Models
{
    public class CohortSeries
    {
        public int Id { get; set; }
        public int CohortId { get; set; }
        public Cohort? Cohort { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Summary { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
