namespace Backend.Dto.Document
{
    public class DocumentDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Data { get; set; }
        public int? ProjectId { get; set; }
    }
}
