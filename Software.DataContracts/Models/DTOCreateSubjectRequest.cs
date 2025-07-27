namespace Software.DataContracts.Models
{
    using System.ComponentModel.DataAnnotations;

    public class DTOCreateSubjectRequest
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        [Required]
        public string Code { get; set; } = string.Empty;
        [Required]
        public int DurationInMonths { get; set; }
        [Required]
        public double Cost { get; set; }
    }
}
