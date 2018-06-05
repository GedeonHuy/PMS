using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PMS.Models;
using PMS.Models.TaskingModels;

namespace PMS.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationRole>().ToTable("ApplicationRole");
            builder.Entity<ApplicationUser>().ToTable("ApplicationUser");
            builder.Entity<Announcement>().ToTable("Announcement");
            builder.Entity<AnnouncementUser>().ToTable("AnnouncementUser");
            builder.Entity<Board>().ToTable("Board");
            builder.Entity<BoardEnrollment>().ToTable("BoardErollment");
            builder.Entity<Enrollment>().ToTable("Enrollment");
            builder.Entity<Grade>().ToTable("Grade");
            builder.Entity<Group>().ToTable("Group");
            builder.Entity<Lecturer>().ToTable("Lecturer");
            builder.Entity<Project>().ToTable("Project");
            builder.Entity<Student>().ToTable("Student");
            builder.Entity<UploadedFile>().ToTable("UploadedFile");
            builder.Entity<Major>().ToTable("Major");
            builder.Entity<Quarter>().ToTable("Quarter");
            builder.Entity<BoardRole>().ToTable("BoardRole");
            builder.Entity<Excel>().ToTable("Excel");
            builder.Entity<Tag>().ToTable("Tag");
            builder.Entity<TagProject>().ToTable("TagProject");
            builder.Entity<Recommendation>().ToTable("Recommendation");

            //TaskingModels
            builder.Entity<Activity>().ToTable("Activity");
            builder.Entity<Comment>().ToTable("Comment");
            builder.Entity<Status>().ToTable("Status");
            builder.Entity<Models.TaskingModels.Task>().ToTable("Task");
            builder.Entity<TaskItem>().ToTable("TaskItem");
        }

        public DbSet<ApplicationRole> ApplicationRole { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<Announcement> Announcement { get; set; }
        public DbSet<AnnouncementUser> AnnouncementUser { get; set; }
        public DbSet<Board> Boards { get; set; }
        public DbSet<BoardEnrollment> BoardEnrollments { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Lecturer> Lecturers { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<UploadedFile> UploadedFiles { get; set; }
        public DbSet<Major> Majors { get; set; }
        public DbSet<Quarter> Quarters { get; set; }
        public DbSet<BoardRole> BoardRoles { get; set; }
        public DbSet<Excel> Excels { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<TagProject> TagProjects { get; set; }
        public DbSet<Recommendation> Recommendations { get; set; }

        //TaskingModels
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Models.TaskingModels.Task> Tasks { get; set; }
        public DbSet<TaskItem> TaskItems { get; set; }

    }
}
