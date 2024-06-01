using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Dto.Account
{
    public class UserDto
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public bool IsAdmin { get; set; } = false;
    }
}
