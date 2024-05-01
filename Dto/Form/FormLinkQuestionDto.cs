namespace Backend.Dto.Form
{
    public class FormLinkQuestionDto
    {
        public int Id { get; set; }
        public int FormId { get; set; }
        public int QuestionId { get; set; }
        public int displayOrder { get; set; }
    }
}
