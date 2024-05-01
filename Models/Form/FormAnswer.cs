using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models.Form
{
    public class FormAnswer
    {
        public int Id { get; set; }
        [ForeignKey("FormResponse")]
        public int FormResponseId { get; set; }
        public virtual FormResponse? FormResponse { get; set; }
        [ForeignKey("FormQuestion")]
        public int FormQuestionId { get; set; }// maybe remove this??
        public virtual FormQuestion? FormQuestion { get; set; }
        public string? Response {  get; set; } // this should be based on the question type. if it is a text box then just a paragraph response but if it is a choice question it should be linked to the question options table. how do you do that????
    }
}
