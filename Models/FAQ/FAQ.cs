using System.ComponentModel.DataAnnotations;

namespace Backend.Models.FAQ
{
    public class FAQ
    {
        public int Id { get; set; }

        [Required]
        public string Question { get; set; }

        [Required]
        public string Response { get; set; }
    }
}
