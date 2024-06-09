using System.ComponentModel.DataAnnotations;

namespace Backend.Models.FAQ
{
    public class Question
    {
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }
    }
}
