namespace Software.DataContracts.Models
{
    using System.ComponentModel.DataAnnotations;

    public class DTORegisterationRequest
    {
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string UserName { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
