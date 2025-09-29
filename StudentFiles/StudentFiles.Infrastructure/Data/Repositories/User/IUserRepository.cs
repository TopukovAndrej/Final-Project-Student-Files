namespace StudentFiles.Infrastructure.Data.Repositories.User
{
    using StudentFiles.Contracts.Models.Result;
    using StudentFiles.Infrastructure.Common.Interfaces;
    using StudentFiles.Infrastructure.Data.Models;

    public interface IUserRepository : IRepository<User>
    {
        public Task<Result<Domain.Entities.User.User>> GetUserByUidAsync(Guid userUid);

        public void UpdateUser(Domain.Entities.User.User user);
    }
}
