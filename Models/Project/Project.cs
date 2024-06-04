﻿using Backend.Models.Account;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models.Project
{
    public class Project
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Objective { get; set; } = string.Empty ;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Status { get; set; }
        [ForeignKey("User")]
        public string? UserId { get; set; }
        public virtual User? User { get; set; }
        public virtual ICollection<Task>? Tasks { get; set; }
        public virtual ICollection<Models.Form.Form>? Forms { get; set; }
        public virtual ICollection<Models.Document.Document>? Documents { get; set; }
        public virtual ICollection<UserProject>? UserProjects { get; set; }

    }
}