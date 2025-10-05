namespace StudentFiles.Infrastructure.Database.Context
{
    using Microsoft.EntityFrameworkCore;
    using StudentFiles.Infrastructure.Data.Models;

    public interface IStudentFilesDbContext
    {
        public DbSet<User> Users { get; }

        public DbSet<Course> Courses { get; }

        public DbSet<Grade> Grades { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
