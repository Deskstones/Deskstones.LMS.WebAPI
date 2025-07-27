namespace Software.DataContracts.Models
{
    using System.ComponentModel.DataAnnotations;

    public class DTOUpdateSubjectRequest
    {
        [Required]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Code { get; set; }
        public int? DurationInMonths { get; set; }
        public double? Cost { get; set; }
    }
}
