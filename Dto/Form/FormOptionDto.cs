using Backend.Models.Form;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Dto.Form
{
    public class FormOptionsDto
    {
        public int Id { get; set; }
        public int FormQuestionId { get; set; }
        public virtual FormQuestion? FormQuestions { get; set; }
        public string? OptionText { get; set; }
    }
}
