using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models.Form
{
    public class Form
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string Description { get; set; } = string.Empty;
        [ForeignKey("Project")]
        public int ProjectId { get; set; }
        public virtual Project? Project { get; set; }

        public virtual ICollection<FormQuestion>? FormQuestions { get; set; }
        public virtual ICollection<FormResponse>? FormResponses { get; set; }
    }

}
