namespace StudentFiles.Infrastructure.DbContext
{
    public interface IStudentFilesDbContext
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
