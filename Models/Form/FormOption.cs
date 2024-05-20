using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models.Form
{
    public class FormOption
    {
        public int Id { get; set; }
        [ForeignKey("FormQuestion")]
        public int FormQuestionId { get; set; }
        public virtual FormQuestion? FormQuestion { get; set; }
        public string? label { get; set; }
    }
}
