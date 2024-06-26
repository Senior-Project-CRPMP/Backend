﻿using Backend.Models;
using Backend.Models.Form;
using Backend.Models.Document;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Backend.Models;
using Backend.Models.Account;
using Backend.Models.FileUpload;
using Backend.Models.Project;
using System.Reflection.Emit;
using Backend.Models.FAQ;

namespace Backend.Data
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Models.Project.Task> Tasks { get; set; }
        public DbSet<Form> Forms { get; set; }
        public DbSet<FormQuestion> FormQuestions { get; set; }
        public DbSet<FormOption> FormOptions { get; set; }
        public DbSet<FormFileStorage> FormFileStorages { get; set; }
        public DbSet<FormResponse> FormResponses { get; set; }
        public DbSet<FormAnswer> FormAnswers { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<FileUpload> FileUploads { get; set; }
        public DbSet<ProfilePicUpload> ProfilePicUploads { get; set; }
        public DbSet<ChatRoom> ChatRooms { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }
        public DbSet<ChatRoomParticipant> ChatRoomParticipants { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<UserProject> UserProjects { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<FAQ> FAQs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Task and Project relationship
            builder.Entity<Models.Project.Task>()
                .HasOne(t => t.Project)
                .WithMany(p => p.Tasks)
                .HasForeignKey(t => t.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            // Project and UserProject relationship
            builder.Entity<UserProject>()
                .HasOne(up => up.Project)
                .WithMany(p => p.UserProjects)
                .HasForeignKey(up => up.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<UserProject>()
                .HasOne(up => up.User)
                .WithMany(u => u.UserProjects)
                .HasForeignKey(up => up.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Project and User relationship
            builder.Entity<Project>()
                .HasOne(p => p.User)
                .WithMany()
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.SetNull);  // Change to SetNull or Restrict

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
                .OnDelete(DeleteBehavior.NoAction);

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

            // Chat relationships
            builder.Entity<ChatRoomParticipant>()
            .HasKey(crp => new { crp.ChatRoomId, crp.UserId });

            builder.Entity<ChatRoomParticipant>()
                .HasOne(crp => crp.ChatRoom)
                .WithMany(cr => cr.Participants)
                .HasForeignKey(crp => crp.ChatRoomId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ChatRoomParticipant>()
                .HasOne(crp => crp.User)
                .WithMany()
                .HasForeignKey(crp => crp.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configuring relationships for Message
            builder.Entity<ChatMessage>()
                .HasOne(m => m.ChatRoom)
                .WithMany(cr => cr.Messages)
                .HasForeignKey(m => m.ChatRoomId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ChatMessage>()
                 .HasOne(cm => cm.User)
                 .WithMany()
                 .HasForeignKey(cm => cm.UserId)
                 .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<FileUpload>()
                .HasOne(fu => fu.Project)
                .WithMany(p => p.FileUploads)
                .HasForeignKey(fu => fu.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
