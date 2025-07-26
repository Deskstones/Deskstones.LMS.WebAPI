namespace Deskstones.LMS.Infrastructure.Models
{
    public class UserAddress
    {
        public int Id { get; set; }
        public int UserProfileId { get; set; }
        public UserProfile? UserProfile { get; set; }
        public string Street { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
