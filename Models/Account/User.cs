using Backend.Models.Project;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


namespace Backend.Models.Account
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        [Column(TypeName = "datetime")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public bool IsAdmin { get; set; } = false;
        public bool IsNotified { get; set; } = true;
        public bool IsVisible { get; set; } = true;
        public string? Bio { get; set; }
        public virtual ICollection<UserProject>? UserProjects { get; set; }

        // Add Refresh Token and its expiration
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public virtual ICollection<ChatRoomParticipant>? ChatRooms { get; set; }
    }
}
