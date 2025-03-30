namespace StudentFiles.Infrastructure.Database.Context
{
    using Microsoft.EntityFrameworkCore;
    using StudentFiles.Infrastructure.Data.Models;

    public interface IStudentFilesReadonlyDbContext
    {
        public DbSet<User> Users { get; }
    }
}
