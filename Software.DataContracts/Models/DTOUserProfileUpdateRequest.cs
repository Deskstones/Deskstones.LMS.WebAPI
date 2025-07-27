namespace Software.DataContracts.Models
{
    using Microsoft.AspNetCore.Http;
    using Software.DataContracts.Shared;
    using System.ComponentModel.DataAnnotations;

    public class DTOUserProfileUpdateRequest
    {
        [Required]
        public int UserId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string MiddleName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Bio { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public IFormFile? ProfilePicture { get; set; }
        public DTOUserAddress? Address { get; set; }
        public DTOUserSocial? Social { get; set; }
    }
}
