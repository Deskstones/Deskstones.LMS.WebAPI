namespace Deskstones.LMS.Infrastructure.Models
{
    public class TeacherProfile
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public AppUser User { get; set; } = null!;
        public string Bio { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ICollection<CourseSubject> Subjects { get; set; } = new List<CourseSubject>();
        public ICollection<Cohort> Cohorts { get; set; } = new List<Cohort>();
    }
}
