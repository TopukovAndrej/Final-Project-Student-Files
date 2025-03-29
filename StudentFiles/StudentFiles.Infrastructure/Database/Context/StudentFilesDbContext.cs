namespace StudentFiles.Infrastructure.Database.Context
{
    using Microsoft.EntityFrameworkCore;
    using StudentFiles.Infrastructure.Common.Configurations;
    using StudentFiles.Infrastructure.Data.Models;

    public class StudentFilesDbContext : DbContext, IStudentFilesDbContext
    {
        public DbSet<User> Users { get; set; }

        public StudentFilesDbContext(DbContextOptions<StudentFilesDbContext> options) : base(options: options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(configuration: new UserConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
