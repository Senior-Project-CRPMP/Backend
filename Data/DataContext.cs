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


        public DbSet<ProjectModel> Projects { get; set; }
        public DbSet<TaskModel> Tasks { get; set; }
        public DbSet<UserModel> Users {  get; set; }
        public DbSet<UserInfoModel> UserInfo { get; set; }
        public DbSet<UserProject> UserProjects { get; set; }
        public DbSet<UserTask> UserTasks { get; set; }
        public DbSet<RoleModel> Roles {  get; set; }
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
            builder.Entity<UserProject>()
                .HasKey(up => new { up.UserId, up.ProjectId });
            builder.Entity<UserProject>()
                .HasOne(u => u.User)
                .WithMany(up => up.UserProjects)
                .HasForeignKey(u => u.UserId);
            builder.Entity<UserProject>()
                .HasOne(p => p.Project)
                .WithMany(up => up.UserProjects)
                .HasForeignKey(p => p.ProjectId);
            builder.Entity<UserTask>()
                .HasKey(ut => new { ut.UserId, ut.TaskId });
            builder.Entity<UserTask>()
                .HasOne(u => u.User)
                .WithMany(ut => ut.UserTasks)
                .HasForeignKey(u => u.UserId);
            builder.Entity<UserTask>()
                .HasOne(t => t.Task)
                .WithMany(ut => ut.UserTasks)
                .HasForeignKey(t => t.TaskId);

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
