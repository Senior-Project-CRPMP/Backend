using Backend.Models;
using Backend.Models.Form;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Backend.Data
{
    public class DataContext : IdentityDbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) 
        {
        
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Models.Task> Tasks { get; set; }
        public DbSet<Form> Forms  { get; set; }
        public DbSet<FormQuestion> FormQuestions  { get; set; }
        public DbSet<FormLinkQuestion> FormLinkQuestions  { get; set; }
        public DbSet<FormOption> FormOptions { get; set; }
        public DbSet<FormFileStorage> FormFileStorages { get; set; }
        public DbSet<FormResponse> FormResponses { get; set; }
        public DbSet<FormAnswer> FormAnswers { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Models.Task>()
                .HasOne(t => t.Project)
                .WithMany(p => p.Tasks)
                .HasForeignKey(t => t.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<FormLinkQuestion>()
               .HasKey(fq => new { fq.FormId, fq.FormQuestionId });

            builder.Entity<FormLinkQuestion>()
                .HasOne(fq => fq.Form)
                .WithMany(f => f.FormLinkQuestions)
                .HasForeignKey(fq => fq.FormId);

            builder.Entity<FormLinkQuestion>()
                .HasOne(fq => fq.FormQuestion)
                .WithMany()
                .HasForeignKey(fq => fq.FormQuestionId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<FormOption>()
                .HasOne(o => o.FormQuestion)
                .WithMany(q => q.FormOptions)
                .HasForeignKey(o => o.FormQuestionId);

            builder.Entity<FormFileStorage>()
                .HasOne(fs => fs.FormResponse)
                .WithMany(fr => fr.FormFileStorages)
                .HasForeignKey(fs => fs.FormResponseId);

            builder.Entity<FormAnswer>()
                .HasOne(a => a.FormResponse)
                .WithMany(fr => fr.FormAnswers)
                .HasForeignKey(a => a.FormResponseId)
                .OnDelete(DeleteBehavior.NoAction);

        }

    }
}
