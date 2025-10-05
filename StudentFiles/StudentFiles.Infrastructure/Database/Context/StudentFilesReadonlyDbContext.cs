namespace StudentFiles.Infrastructure.Database.Context
{
    using Microsoft.EntityFrameworkCore;
    using StudentFiles.Infrastructure.Common.Configurations;
    using StudentFiles.Infrastructure.Data.Models;

    public class StudentFilesReadonlyDbContext : DbContext, IStudentFilesReadonlyDbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Course> Courses { get; set; }

        public StudentFilesReadonlyDbContext(DbContextOptions<StudentFilesReadonlyDbContext> options) : base(options: options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(configuration: new UserConfiguration());
            modelBuilder.ApplyConfiguration(configuration: new CourseConfiguration());
            modelBuilder.ApplyConfiguration(configuration: new GradeConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
