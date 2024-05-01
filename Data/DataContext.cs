using Backend.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<TaskModel>()
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
        }

    }
}
