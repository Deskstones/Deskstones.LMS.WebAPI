using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deskstones.LMS.Infrastructure.Models
{
    public class Cohort
    {
        public int Id { get; set; }
        public int TeacherProfileId { get; set; }
        public TeacherProfile TeacherProfile { get; set; } = null!;
        public int SubjectId { get; set; }
        public CourseSubject Subject { get; set; } = null!;
        public string CohourtName { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string CohortAddress { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ICollection<CohortModule> CohortModules { get; set; } = new List<CohortModule>();

    }
}
