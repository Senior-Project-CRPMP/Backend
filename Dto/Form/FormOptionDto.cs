using Backend.Models.Form;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Dto.Form
{
    public class FormOptionDto
    {
        public int Id { get; set; }
        public int FormQuestionId { get; set; }
        public string? Label { get; set; }
    }
}
