namespace Backend.Dto.Form
{
    public class FormDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public int ProjectId { get; set; }
    }
}
