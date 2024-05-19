using Backend.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Dto.Form
{
    public class FormDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string Description { get; set; } = string.Empty;
        public int ProjectId { get; set; }
    }
}
