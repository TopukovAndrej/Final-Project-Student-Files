namespace StudentFiles.Infrastructure.Common.Interfaces
{
    public interface IRepository<T>
    {
        Task SaveChangesAsync();
    }
}
