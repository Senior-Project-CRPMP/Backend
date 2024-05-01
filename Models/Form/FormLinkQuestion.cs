using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models.Form
{
    public class FormLinkQuestion
    {
        public int Id { get; set; }
        [ForeignKey("Form")]
        public int FormId { get; set; }
        public Form? Form { get; set; }
        [ForeignKey("FormQuestion")]
        public int FormQuestionId { get; set; }
        public FormQuestion? FormQuestion { get; set; }
        public int displayOrder { get; set; }
    }
}
