namespace Backend.Dto.Form
{
    public class FormResponseDto
    {
        public int Id { get; set; }
        public int FormId { get; set; }
        public ICollection<FormAnswerDto> Answers { get; set; }
        public ICollection<IFormFile>? Files { get; set; }
    }
}
