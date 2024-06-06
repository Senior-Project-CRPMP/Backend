namespace Backend.Dto.Form
{
    public class FormAnswerDto
    {
        public int Id { get; set; }
        public int FormResponseId { get; set; }
        public int FormQuestionId { get; set; }
        public int FormOptionId { get; set; }
        public string? Response { get; set; }
    }
}
