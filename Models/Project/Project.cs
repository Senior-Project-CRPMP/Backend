using Backend.Models.Account;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models.Project
{
    public class Project
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Objective { get; set; } = string.Empty ;
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; }
        public bool IsDone { get; set; } = false;
        [ForeignKey("User")]
        public string? UserId { get; set; }
        public bool IsVisible { get; set; } = true;
        public virtual User? User { get; set; }
        public virtual ICollection<Task>? Tasks { get; set; }
        public virtual ICollection<Models.Form.Form>? Forms { get; set; }
        public virtual ICollection<Models.Document.Document>? Documents { get; set; }
        public virtual ICollection<UserProject>? UserProjects { get; set; }

        public virtual ICollection<Models.FileUpload.FileUpload>? FileUploads { get; set; }

    }
}
