using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PMS.Models;

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
            builder.Entity<Council>().ToTable("Council");
            builder.Entity<CouncilEnrollment>().ToTable("CouncilErollment");
            builder.Entity<Enrollment>().ToTable("Enrollment");
            builder.Entity<Grade>().ToTable("Grade");
            builder.Entity<Group>().ToTable("Group");
            builder.Entity<Lecturer>().ToTable("Lecturer");
            builder.Entity<Project>().ToTable("Project");
            builder.Entity<Student>().ToTable("Student");
            builder.Entity<UploadedFile>().ToTable("UploadedFile");
        }
        public DbSet<Council> Councils { get; set; }
        public DbSet<CouncilEnrollment> CouncilEnrollments { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Lecturer> Lecturers { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<UploadedFile> UploadedFiles { get; set; }
    }
}
