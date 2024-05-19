using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models.Form
{
    public class FormFileStorage
    {
        public int Id { get; set; }

        public string? Name { get; set; }
        public string? Path { get; set; }

        [ForeignKey("FormResponse")]
        public int FormResponseId { get; set; }
        public virtual FormResponse? FormResponse { get; set; }
    }

}
