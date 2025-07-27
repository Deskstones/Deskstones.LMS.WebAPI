namespace Software.DataContracts.Models
{
    public class DTOCreateSubjectRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public int DurationInMonths { get; set; }
        public double Cost { get; set; }
    }
}
