using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models.Form
{
    public class FormResponse
    {
        public int Id { get; set; }

        [ForeignKey("Form")]
        public int FormId { get; set; }
        public virtual Form? Form { get; set; }

        public virtual ICollection<FormFileStorage>? FormFileStorages { get; set; }
        public virtual ICollection<FormAnswer>? FormAnswers { get; set; }
    }

}
