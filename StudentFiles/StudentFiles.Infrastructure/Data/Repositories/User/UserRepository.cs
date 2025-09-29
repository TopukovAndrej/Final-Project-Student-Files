namespace StudentFiles.Infrastructure.Data.Repositories.User
{
    using Microsoft.EntityFrameworkCore;
    using StudentFiles.Contracts.Common;
    using StudentFiles.Contracts.Models.Result;
    using StudentFiles.Infrastructure.Data.Mappers;
    using StudentFiles.Infrastructure.Data.Models;
    using StudentFiles.Infrastructure.Database.Context;

    public class UserRepository : IUserRepository
    {
        private readonly IStudentFilesDbContext _dbContext;

        public UserRepository(IStudentFilesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<Domain.Entities.User.User>> GetUserByUidAsync(Guid userUid)
        {
            User? dbUser = await _dbContext.Users.AsNoTracking().SingleOrDefaultAsync(predicate: x => !x.IsDeleted
                                                                                                   && x.Uid == userUid);

            if (dbUser == null)
            {
                return Result<Domain.Entities.User.User>.Failed(error: new Error(Code: ErrorCodes.UserNotFound, Message: ErrorMessage.UserNotFound),
                                                                resultType: ResultType.NotFound);
            }

            return DataToDomainMapper.MapUserDataToDomain(dbUser: dbUser);
        }

        public void UpdateUser(Domain.Entities.User.User user)
        {
            User dbResource = DomainToDataMapper.MapUserDomainToData(domainUser: user);

            _dbContext.Users.Update(dbResource);
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
