namespace Deskstones.LMS.Infrastructure.Models
{
    public class AttachmentBucket
    {
        public int Id { get; set; }
        public string AttachmentUrl { get; set; } = string.Empty;
        public string Reference { get; set; } = string.Empty;
        public int ReferenceId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
