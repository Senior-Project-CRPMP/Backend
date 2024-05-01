namespace Backend.Dto.Form
{
    public class FormQuestionDto
    {
        public int Id { get; set; }
        public int FormId { get; set; }
        public string? InputType { get; set; }
        public string? InputLabel { get; set; }
    }
}
