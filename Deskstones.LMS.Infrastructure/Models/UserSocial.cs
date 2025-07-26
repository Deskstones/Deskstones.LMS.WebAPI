namespace Deskstones.LMS.Infrastructure.Models
{
    public class UserSocial
    {
        public int Id { get; set; }
        public int UserProfileId { get; set; }
        public UserProfile? UserProfile { get; set; }
        public string LinkedInUrl { get; set; } = string.Empty;
        public string GitHubUrl { get; set; } = string.Empty;
        public string TwitterUrl { get; set; } = string.Empty;
        public string WebsiteUrl { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
