namespace Deskstones.LMS.Infrastructure.Models
{
    public class TeacherProfile
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public string Bio { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ICollection<Subject> Subjects { get; set; } = new List<Subject>();
        public ICollection<Cohort> Cohorts { get; set; } = new List<Cohort>();
    }
}
