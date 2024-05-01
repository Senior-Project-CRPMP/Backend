namespace Backend.Dto.Form
{
    public class FileStorageDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Path { get; set; }
        public int FormResponseId { get; set; }
    }
}
