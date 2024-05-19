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
        public int FormQuestionId { get; set; }
        public virtual FormQuestion? FormQuestion { get; set; }

        [ForeignKey("FormOption")]
        public int? FormOptionId { get; set; }
        public virtual FormOption? FormOption { get; set; }

        public string? Response { get; set; }
    }



}
