using Backend.Models;
using Backend.Models.Form;
using Backend.Models.Document;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data
{
    public class DataContext : IdentityDbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Models.Task> Tasks { get; set; }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<UserInfoModel> UserInfo { get; set; }
        public DbSet<UserProject> UserProjects { get; set; }
        public DbSet<UserTask> UserTasks { get; set; }
        public DbSet<RoleModel> Roles { get; set; }
        public DbSet<Form> Forms { get; set; }
        public DbSet<FormQuestion> FormQuestions { get; set; }
        public DbSet<FormOption> FormOptions { get; set; }
        public DbSet<FormFileStorage> FormFileStorages { get; set; }
        public DbSet<FormResponse> FormResponses { get; set; }
        public DbSet<FormAnswer> FormAnswers { get; set; }
        public DbSet<Document> Documents { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Task and Project relationship
            builder.Entity<Models.Task>()
                .HasOne(t => t.Project)
                .WithMany(p => p.Tasks)
                .HasForeignKey(t => t.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            // UserProject relationships
            builder.Entity<UserProject>()
                .HasKey(up => new { up.UserId, up.ProjectId });
            builder.Entity<UserProject>()
                .HasOne(up => up.User)
                .WithMany(u => u.UserProjects)
                .HasForeignKey(up => up.UserId);
            builder.Entity<UserProject>()
                .HasOne(up => up.Project)
                .WithMany(p => p.UserProjects)
                .HasForeignKey(up => up.ProjectId);

            // UserTask relationships
            builder.Entity<UserTask>()
                .HasKey(ut => new { ut.UserId, ut.TaskId });
            builder.Entity<UserTask>()
                .HasOne(ut => ut.User)
                .WithMany(u => u.UserTasks)
                .HasForeignKey(ut => ut.UserId);
            builder.Entity<UserTask>()
                .HasOne(ut => ut.Task)
                .WithMany(t => t.UserTasks)
                .HasForeignKey(ut => ut.TaskId);

            // FormQuestion and Form relationship
            builder.Entity<FormQuestion>()
                .HasOne(fq => fq.Form)
                .WithMany(f => f.FormQuestions)
                .HasForeignKey(fq => fq.FormId)
                .OnDelete(DeleteBehavior.Cascade);

            // FormOption and FormQuestion relationship
            builder.Entity<FormOption>()
                .HasOne(fo => fo.FormQuestion)
                .WithMany(fq => fq.FormOptions)
                .HasForeignKey(fo => fo.FormQuestionId)
                .OnDelete(DeleteBehavior.Cascade);

            // FormResponse and Form relationship
            builder.Entity<FormResponse>()
                .HasOne(fr => fr.Form)
                .WithMany(f => f.FormResponses)
                .HasForeignKey(fr => fr.FormId)
                .OnDelete(DeleteBehavior.NoAction); // NoAction to prevent multiple cascade paths

            // FormFileStorage and FormResponse relationship
            builder.Entity<FormFileStorage>()
                .HasOne(fs => fs.FormResponse)
                .WithMany(fr => fr.FormFileStorages)
                .HasForeignKey(fs => fs.FormResponseId)
                .OnDelete(DeleteBehavior.Cascade);

            // FormAnswer relationships
            builder.Entity<FormAnswer>()
                .HasOne(fa => fa.FormResponse)
                .WithMany(fr => fr.FormAnswers)
                .HasForeignKey(fa => fa.FormResponseId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<FormAnswer>()
                .HasOne(fa => fa.FormQuestion)
                .WithMany(fq => fq.FormAnswers)
                .HasForeignKey(fa => fa.FormQuestionId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<FormAnswer>()
                .HasOne(fa => fa.FormOption)
                .WithMany()
                .HasForeignKey(fa => fa.FormOptionId)
                .OnDelete(DeleteBehavior.SetNull);

            // Document and Project relationship
            builder.Entity<Document>()
                .HasOne(d => d.Project)
                .WithMany(p => p.Documents)
                .HasForeignKey(d => d.ProjectId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
